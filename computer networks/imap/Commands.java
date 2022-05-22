import java.io.*;
import java.util.*;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.SimpleFileVisitor;
import java.nio.file.FileVisitResult;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.util.zip.ZipOutputStream;
import java.nio.file.attribute.BasicFileAttributes;
import java.util.zip.ZipEntry;

public class Commands {

    private BufferedWriter bufferedWriter;
    private BufferedReader bufferedReader;

    public Commands(BufferedWriter bufferedWriter, BufferedReader bufferedReader) {

        this.bufferedWriter = bufferedWriter;
        this.bufferedReader = bufferedReader;
    }

    private void Send(String command) throws IOException {
        bufferedWriter.write(command + "\r\n");
        bufferedWriter.flush();
    }

    public void Login(User user) throws IOException {
        String message = ". LOGIN " + user.getEmail() + " " + user.getPass();
        String response;

        Send(message);

        boolean loggedIn = true;

        while ((response = bufferedReader.readLine()).contains("(Success)") == false) {
            if (response.contains("(Failure)")) {
                System.out.println("LOG IN: Failure");
                loggedIn = false;
                break;
            }
        }

        if (loggedIn) {
            System.out.println("LOG IN: Success");
        }
    }

    public void Logout() throws IOException {
        String message = ". LOGOUT";
        String response;

        Send(message);

        boolean loggedOut = true;

        while ((response = bufferedReader.readLine()).contains("(Success)") == false) {
            if (response.contains("(Failure)")) {
                System.out.println("LOG OUT: Failure");
                loggedOut = false;
                break;
            }
        }

        if (loggedOut) {
            System.out.println("LOG OUT: Success");
        }
    }

    public void SelectInbox() throws IOException {
        String message = ". SELECT INBOX";
        String response;

        Send(message);

        while ((response = bufferedReader.readLine()).contains("(Success)") == false) {
            if (response.contains("BAD") || response.contains("NO")) {
                System.out.println("SELECT INBOX: Failure");
                break;
            }
        }
        System.out.println("SELECT INBOX: Success");
    }

    private int InboxCount() throws IOException {
        String message = ". SELECT INBOX";
        String response;
        int n = 0;

        Send(message);

        while ((response = bufferedReader.readLine()).contains("(Success)") == false) {
            if (response.contains("BAD") || response.contains("NO")) {
                System.out.println("SELECT INBOX: Failure");
                break;
            }

            if (response.contains("EXISTS")) {
                Matcher matcher = Pattern.compile("\\d+").matcher(response);
                matcher.find();
                n = Integer.valueOf(matcher.group());
                System.out.println("SELECT INBOX: Success (exists)");
            }
        }

        return n;
    }

    public void FetchEmails() throws IOException {
        String message = ". FETCH 1:" + InboxCount() + " BODY[HEADER.FIELDS (SUBJECT FROM DATE)]";
        String response;

        Send(message);

        while ((response = bufferedReader.readLine()).contains(". OK Success") == false) {
            if (response.contains("BAD") || response.contains("NO")) {
                System.out.println("FETCH: Failure");
                break;
            }

            if (response.contains("FETCH")) {
                System.out.println("");
                Matcher matcher = Pattern.compile("\\d+").matcher(response);
                matcher.find();
                System.out.println("FETCH: Success (" + matcher.group() + ")");
            }

            if (response.contains("From") || response.contains("Subject") || response.contains("Date")) {
                System.out.println(response);
            }
        }
    }

    public void FetchOneEmail(String id) throws IOException {
        String message = ". FETCH " + id + " BODY.PEEK[1]\r\n";
        String response;

        Send(message);

        while (!(response = bufferedReader.readLine()).contains("Success")) {
            if (response.contains("BAD") || response.contains("NO")) {
                System.out.println("FETCH: Failure");
                break;
            }

            System.out.println(response);
        }
    }
    
    private String FetchAttachmentName(String id) throws IOException {
        String message;
        String response;
        String result = "a";
        String find = "FILENAME";

        message = ". FETCH " + id + " BODYSTRUCTURE";
        Send(message);

        while ((response = bufferedReader.readLine()) != null) {

            if (response.contains("Success")) {
                break;
            }

            if (response.contains("FILENAME")) {
                int i = response.indexOf(find);
                if (i > 0) {
                    result = response.substring(i + find.length() + 2);
                    result = result.replace("\"", "");

                    int i2 = result.indexOf("))");
                    if (i2 > 0) {
                        result = result.substring(0, i2);
                    }
                }
            }
        }

        return result;
    }

    public String[] FetchAttachmentsNames(String id) throws IOException {
        String message;
        String response;
        String result = "a";
        String find = "FILENAME";

        List<String> list = new ArrayList<String>();

        message = ". FETCH " + id + " BODYSTRUCTURE";
        Send(message);

        while ((response = bufferedReader.readLine()) != null) {

            if (response.contains("Success")) {
                break;
            }

            while (response.contains("FILENAME")) {
                int i = response.indexOf(find);
                response = response.replaceFirst("FILENAME", "");

                if (i > 0) {
                    result = response.substring(i + 2);
                    result = result.replace("\"", "");

                    int i2 = result.indexOf("))");
                    if (i2 > 0) {
                        result = result.substring(0, i2);
                    }
                }
                list.add(result);
            }
        }

        return list.toArray(new String[0]);
    }

    public void FetchAttachment(String id) throws IOException {
        String message = ". FETCH " + id + " BODY.PEEK[2]";
        String fileName = FetchAttachmentName(id);
        File file = new File("files/" + fileName);
        OutputStream fileOutputStream = new FileOutputStream(file);
        Send(message);

        String res;
        res = bufferedReader.readLine();

        while (true) {
            res = bufferedReader.readLine();
            String find = "OK Success";
            if (res.indexOf(find) > 0) {
                break;
            }

            byte[] decoded = Base64.getDecoder().decode(res.replace(")", ""));
            fileOutputStream.write(decoded);
        }

        fileOutputStream.close();
    }

    // function to delete subdirectories and files
    public static void deleteDirectory(File file)
    {
        // store all the paths of files and folders present
        // inside directory
        for (File subfile : file.listFiles()) {
  
            // if it is a subfolder,e.g Rohan and Ritik,
            // recursiley call function to empty subfolder
            if (subfile.isDirectory()) {
                deleteDirectory(subfile);
            }
  
            // delete files and empty subfolders
            subfile.delete();
        }

        file.delete();
    }

    private static void zipFolderStructure(String sourceFolder, String zipFolder){
        // Creating a ZipOutputStream by wrapping a FileOutputStream
        try (FileOutputStream fos = new FileOutputStream(zipFolder); 
             ZipOutputStream zos = new ZipOutputStream(fos)) {
          Path sourcePath = Paths.get(sourceFolder);
          // Walk the tree structure using WalkFileTree method
          Files.walkFileTree(sourcePath, new SimpleFileVisitor<Path>(){
            @Override
            // Before visiting the directory create the directory in zip archive
            public FileVisitResult preVisitDirectory(final Path dir, final BasicFileAttributes attrs) throws IOException {
              // Don't create dir for root folder as it is already created with .zip name 
              if(!sourcePath.equals(dir)){
                System.out.println("Directory- " + dir);
                zos.putNextEntry(new ZipEntry(sourcePath.relativize(dir).toString() + "/"));                  
                zos.closeEntry();    
              }
              return FileVisitResult.CONTINUE;
            }
            @Override
            // For each visited file add it to zip entry
            public FileVisitResult visitFile(final Path file, final BasicFileAttributes attrs) throws IOException {
              System.out.println("File Name- " + sourcePath.relativize(file).toString());
              zos.putNextEntry(new ZipEntry(sourcePath.relativize(file).toString()));
              Files.copy(file, zos);
              zos.closeEntry();
              return FileVisitResult.CONTINUE;
            }});
        } catch (IOException e) {
          // TODO Auto-generated catch block
          e.printStackTrace();
        }
      }

    private String FormMessage(String id, int which) {
        return ". FETCH " + id + " BODY.PEEK[" + Integer.toString(which) + "]";
    }

    public void FetchAttachments(String id) throws IOException {
        int i = 2;
        String names = "";
        for (String name : FetchAttachmentsNames(id)) {
            names += name + "__";
            String message = FormMessage(id, i);
            String fileName = name;

            new File("files/"+id+"/").mkdirs();
            File file = new File("files/"+id+"/" + fileName);
            OutputStream fileOutputStream = new FileOutputStream(file);
            Send(message);

            String res;
            res = bufferedReader.readLine();

            while (true) {
                res = bufferedReader.readLine();
                String find = "OK Success";
                if (res.indexOf(find) > 0) {
                    break;
                }

                byte[] decoded = Base64.getDecoder().decode(res.replace(")", ""));
                fileOutputStream.write(decoded);
            }

            fileOutputStream.close();
            i++;
        }

        names += "priedai.zip";
        zipFolderStructure("files/"+id, "files/" + names);

        File file = new File("files/"+id);
        deleteDirectory(file);
    }

    public void Search(String body) throws IOException {
        String message = ". SEARCH BODY \"" + body + "\"";
        String response;

        Send(message);

        while ((response = bufferedReader.readLine()).contains("(Success)") == false) {
            if (response.contains("BAD") || response.contains("NO")) {
                System.out.println("SEARCH BODY: Failure");
                break;
            }

            if (response.contains("SEARCH")) {
                System.out.println("SEARCH BODY: Success");
                System.out.println("Found emails IDs: " + response.replace("* SEARCH", ""));
            }
        }
    }

    public void List(String arg) throws IOException {
        String message = ". LIST \"\" \"" + arg + "*\"";
        String response;

        Send(message);

        while (!(response = bufferedReader.readLine()).contains(". OK Success")) {
            if (response.contains("BAD") || response.contains("NO")) {
                System.out.println("LIST: Failure");
                break;
            }

            System.out.println(response);
        }
    }

    public void Create(String place, String name) throws IOException {
        String message;

        if (place.isEmpty()) {
            message = ". CREATE " + name;
            Send(message);
        } else {
            message = ". CREATE " + place + "/" + name;
            Send(message);
        }
    }

    public void Delete(String place, String name) throws IOException {
        String message;

        if (place.isEmpty()) {
            message = ". DELETE " + name;
            Send(message);
        } else {
            message = ". DELETE " + place + "/" + name;
            Send(message);
        }
    }

    public void Rename(String place, String name, String newName) throws IOException {
        String message;

        if (place.isEmpty()) {
            message = ". RENAME " + name + " " + newName;
            Send(message);
        } else {
            message = ". RENAME " + place + "/" + name + " " + place + "/" + newName;
            ;
            Send(message);
        }
    }

    public void Copy(String newPlace, String idChoice) throws IOException {
        String message;
        String response;

        message = ". COPY " + idChoice + " " + newPlace;
        Send(message);

        while (!(response = bufferedReader.readLine()).contains("Success")) {
            if (response.contains("BAD") || response.contains("NO")) {
                System.out.println("LIST: Failure");
                break;
            }

            System.out.println(response);
        }
    }
}