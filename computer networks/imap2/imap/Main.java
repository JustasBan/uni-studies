import java.io.*;
import javax.net.ssl.SSLSocket;
import javax.net.ssl.SSLSocketFactory;

public class Main {

    public static void main(String[] args) throws IOException {
        User user = new User("imap.gmail.com", 993, "BenoDoverioMailas2@gmail.com", "BenoDoverioMailas1");

        SSLSocketFactory ssl = (SSLSocketFactory) SSLSocketFactory.getDefault();
        SSLSocket sock = (SSLSocket) ssl.createSocket(user.getHost(), user.getPort());

        sock.setEnableSessionCreation(true);
        sock.setUseClientMode(true);
        sock.startHandshake();

        InputStreamReader inputStreamReader = new InputStreamReader(sock.getInputStream());
        BufferedReader bufferedReader = new BufferedReader(inputStreamReader);

        OutputStreamWriter outputStreamWriter = new OutputStreamWriter(sock.getOutputStream());
        BufferedWriter bufferedWriter = new BufferedWriter(outputStreamWriter);

        Commands commands = new Commands(bufferedWriter, bufferedReader);
        commands.Login(user);
        commands.SelectInbox();

        int input = 100;
        BufferedReader r = new BufferedReader(new InputStreamReader(System.in));

        System.out.println("1: Fetch all emails");
        System.out.println("2: Fetch individual email");
        System.out.println("3: Fetch individual email attachments");
        System.out.println("4: Search body");
        System.out.println("5: List emails");
        System.out.println("6: Create folder");
        System.out.println("7: Delete folder");
        System.out.println("8: Rename folder");
        System.out.println("9: Copy email to folder");
        System.out.println("10: attachments count");
        System.out.println("0: log out");

        while (input != 0) {
            input = Integer.parseInt(r.readLine());

            switch (input) {
                case 1:
                    commands.FetchEmails();
                    break;

                case 2:
                    System.out.println("input id:");
                    String input3 = r.readLine();
                    commands.FetchOneEmail(input3);
                    break;

                case 3:
                    System.out.println("input id:");
                    String input4 = r.readLine();
                    commands.FetchAttachment(input4);
                    break;

                case 4:
                    System.out.println("input body:");
                    String input5 = r.readLine();
                    commands.Search(input5);
                    break;

                case 5:
                    System.out.println("input listing:");
                    String input6 = r.readLine();
                    commands.List(input6);
                    break;

                case 6:
                    System.out.println("input place:");
                    String input1 = r.readLine();
                    System.out.println("input name:");
                    String input2 = r.readLine();
                    System.out.println(input1 + " " + input2);
                    commands.Create(input1, input2);
                    break;

                case 7:
                    System.out.println("input place:");
                    String input7 = r.readLine();
                    System.out.println("input name:");
                    String input8 = r.readLine();
                    commands.Delete(input7, input8);
                    break;

                case 8:
                    System.out.println("input place:");
                    String input9 = r.readLine();
                    System.out.println("input old name:");
                    String input10 = r.readLine();
                    System.out.println("input new name:");
                    String input11 = r.readLine();
                    commands.Rename(input9, input10, input11);
                    break;

                case 9:
                    System.out.println("input new place:");
                    String input12 = r.readLine();
                    System.out.println("input id:");
                    String input14 = r.readLine();
                    commands.Copy(input12, input14);
                    break;

                case 10:
                    System.out.println("input id:");
                    String input15 = r.readLine();
                    if(commands.FetchAttachmentsNames(input15).length == 1){
                        commands.FetchAttachment(input15);
                    }
                    else{
                        commands.FetchAttachments(input15);
                    }
                    break;
            }
        }

        commands.Logout();
        r.close();
        bufferedReader.close();
        bufferedWriter.close();
        sock.close();
    }
}