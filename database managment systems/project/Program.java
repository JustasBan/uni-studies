import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;
import java.util.List;

import ProgramUtils.Credentials;
import SqlThings.ExecutingSQL;

public class Program {

  /********************************************************/
  public static void loadDriver() {
    try {
      Class.forName("org.postgresql.Driver");
    } catch (ClassNotFoundException cnfe) {
      System.out.println("Couldn't find driver class!");
      cnfe.printStackTrace();
      System.exit(1);
    }
  }

  /********************************************************/
  public static Connection getConnection() {
    Connection postGresConn = null;
    try {
      postGresConn = DriverManager.getConnection("jdbc:postgresql://pgsql2.mif/studentu", Credentials.userName,
          Credentials.pass);
    } catch (SQLException sqle) {
      System.out.println("Couldn't connect to database!");
      sqle.printStackTrace();
      return null;
    }
    System.out.println("Successfully connected to Postgres Database");

    return postGresConn;
  }
  
  /********************************************************/

  public static void printUI() {
    System.out.println("\nValdymas: ");
    System.out.println("1 - surasti imones ir ju duomens (pagal pav. arba koda)");
    System.out.println("2 - uzregistruoti klienta (imone)");
    System.out.println("3 - atnaujinti produkcija");
    System.out.println("4 - atsisakyti produkcijos");
    System.out.println("0 - baigti darba");
  }

  public static void printList(List<String> imones) {
    for (String string : imones) {
      System.out.println(string);
    }
  }  
  /********************************************************/
  public static void main(String[] args) throws SQLException {
    boolean workDone = false;

    loadDriver();
    Connection con = getConnection();

    while (!workDone) {

      printUI();

      switch (System.console().readLine()) {
        case "0":
          workDone = true;
          break;

        case "1":
          System.out.println("Iveskite pavadinima arba imones koda:");
          List<String> temp = ExecutingSQL.getImones
                          (con, System.console().readLine());
          printList(temp);
          break;

        case "2":
          System.out.println("Dabartiniai duomenys:");
          List<String> temp2 = ExecutingSQL.getKlientai
                          (con);
          printList(temp2);

          System.out.println("Iveskite kliento id, imones pavadinima, imones koda, adresa ir kontakta: ");
          System.out.println("(-1 operacijai nutaukti)");

          int klient_id = Integer.parseInt(System.console().readLine());

          if(klient_id != -1){
            String pavadinimas = System.console().readLine();
            String imones_kodas = System.console().readLine();
            String adresas = System.console().readLine();
            String kontaktas = System.console().readLine();

            ExecutingSQL.newImone(con, klient_id, pavadinimas, imones_kodas, adresas, kontaktas);
          }          
          break;

        case "3":
          System.out.println("Dabartiniai duomenys:");
          List<String> temp3 = ExecutingSQL.getProd(con);
          printList(temp3);
          
          System.out.println("Iveskite ID, kuri norite pakeisti:");
          System.out.println("(-1 operacijai nutaukti)");
          int prod_id = Integer.parseInt(System.console().readLine());

          if(prod_id!=-1){
            System.out.println("Ilgis:");
            int ilgis = Integer.parseInt(System.console().readLine());
            System.out.println("Plotis:");
            int plotis = Integer.parseInt(System.console().readLine());
            System.out.println("Ausktis:");
            int aukstis = Integer.parseInt(System.console().readLine());

            ExecutingSQL.updateProd(con, ilgis, plotis, aukstis, prod_id);
          }
          break;

        case "4":
          
          System.out.println("Dabartiniai duomenys:");
          List<String> temp4 = ExecutingSQL.getProd(con);
          printList(temp4);
          
          System.out.println("Iveskite prod. ID, kurios nebenorite sadelyje");
          System.out.println("(-1 operacijai nutaukti)");
          int klient_id2 = Integer.parseInt(System.console().readLine());
          
          if(klient_id2!=-1){
              ExecutingSQL.deleteUzsak(con, klient_id2);
          }
          break;

        default:
          System.out.println("Nezinoma komanda!");
          break;
      }
    }

    if (null != con) {
      try {
        con.close();
      } catch (SQLException exp) {
        System.out.println("Can not close connection!");
        exp.printStackTrace();
      }
    }

    System.exit(0);

  }
}
