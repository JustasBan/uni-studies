package SqlThings;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

public class ExecutingSQL {

    public static List<String> getImones(Connection postGresConn, String argument) {

        if (postGresConn == null) {
            System.out.println("We should never get here.");
            return null;
        }
        
        List<String> result = new ArrayList<String>();

        Statement stmt = null;
        ResultSet rs = null;

        //naudoti bent 2 tarpusavyje susijusias lenteles:
        try {
            stmt = postGresConn.createStatement();
            rs = stmt.executeQuery(
            "SELECT Pavadinimas, Imones_kodas, Adresas, Kontaktas "+
            "FROM juba7706.imone AS I, juba7706.klientai AS K " +
            "WHERE (Pavadinimas = '" + argument + "'" +
                "OR imones_kodas = '" + argument + "')" +
                "AND I.ID = K.ID");
            
            while (rs.next()!=false) {
                
                result.add(rs.getString(1) + " | " + rs.getString(2) + " | " + rs.getString(3)+ " | " + rs.getString(4));
            }
           
          } catch (SQLException e) {
            System.out.println("SQL Error!");
            e.printStackTrace();
          } finally {
            try {
              if (null != rs)
                rs.close();
              if (null != stmt)
                stmt.close();
            } catch (SQLException exp) {
              System.out.println("Unexpected SQL Error!");
              exp.printStackTrace();
            }
          }
        return result;   
    }

    public static List<String> getKlientai(Connection postGresConn) {

        if (postGresConn == null) {
            System.out.println("We should never get here.");
            return null;
        }
        
        List<String> result = new ArrayList<String>();

        Statement stmt = null;
        ResultSet rs = null;

        //naudoti bent 2 tarpusavyje susijusias lenteles:
        try {
            stmt = postGresConn.createStatement();

            rs = stmt.executeQuery(
            "SELECT I.ID, Pavadinimas, Imones_kodas, Adresas, Kontaktas "+
            "FROM juba7706.imone AS I, juba7706.klientai AS K " +
            "WHERE I.ID = K.ID");
            
            result.add("Imones:");
            while (rs.next()!=false) {
                
                result.add(rs.getString(1) + " | " + rs.getString(2) + " | " + rs.getString(3) + " | " + rs.getString(4)+ " | " + rs.getString(5));
            }
           
          } catch (SQLException e) {
            System.out.println("SQL Error!");
            e.printStackTrace();
          } finally {
            try {
              if (null != rs)
                rs.close();
              if (null != stmt)
                stmt.close();
            } catch (SQLException exp) {
              System.out.println("Unexpected SQL Error!");
              exp.printStackTrace();
            }
          }

        try {
          stmt = postGresConn.createStatement();
          rs = stmt.executeQuery(
          "SELECT A.ID, Vardas, Pavarde, Kontaktas "+
          "FROM juba7706.asmuo AS A, juba7706.klientai AS K " +
          "WHERE A.ID = K.ID");
          
          result.add("Fiziniai asmenys:");
          while (rs.next()!=false) {
              
            result.add(rs.getString(1) + " | " + rs.getString(2) + " | " + rs.getString(3) + " | " + rs.getString(4));
            }
         
        } catch (SQLException e) {
          System.out.println("SQL Error!");
          e.printStackTrace();
        } finally {
          try {
            if (null != rs)
              rs.close();
            if (null != stmt)
              stmt.close();
          } catch (SQLException exp) {
            System.out.println("Unexpected SQL Error!");
            exp.printStackTrace();
          }
        }
        
        return result;   
    }

    public static void newImone(Connection postGresConn, int klientas, String pavadinimas, String imones_kodas, String adresas, String kontaktas) throws SQLException {
        
        if (postGresConn == null) {
            System.out.println("We should never get here.");
        }
        
        Statement stmt = null;
        ResultSet rs = null;

        try {

            //panaudota reali transakcija:
            postGresConn.setAutoCommit(false);

            stmt = postGresConn.createStatement();

            stmt.executeUpdate(
                "INSERT INTO juba7706.klientai " +
                "VALUES( " + klientas + ", "
                           + "'" + kontaktas + "'" +" )"
            );           

            stmt.executeUpdate(
            "INSERT INTO juba7706.imone " + 
            "VALUES( " + klientas + ", "
                       + "'" + pavadinimas + "'" + ", "
                       + "'" +imones_kodas + "'" +", "
                       + "'" +adresas + "'" +" )");
            
            
            postGresConn.commit();
            postGresConn.setAutoCommit(true);

          } catch (SQLException e) {
            System.out.println("SQL Error!");
            e.printStackTrace();

            postGresConn.rollback();
            postGresConn.setAutoCommit(true);
          } finally {
            try {
              if (null != rs)
                rs.close();
              if (null != stmt)
                stmt.close();
            } catch (SQLException exp) {
              System.out.println("Unexpected SQL Error!");
              exp.printStackTrace();
            }
          }
    }

    public static List<String> getProd(Connection postGresConn) {

        if (postGresConn == null) {
            System.out.println("We should never get here.");
            return null;
        }
        
        List<String> result = new ArrayList<String>();

        Statement stmt = null;
        ResultSet rs = null;

        try {
            stmt = postGresConn.createStatement();
            rs = stmt.executeQuery(
                "SELECT ID, Mediena, ilgis, plotis, aukstis "+
                "FROM juba7706.produkcija");
            
            while (rs.next()!=false) {
                
                result.add(rs.getString(1) + " | " + rs.getString(2) + " | " + rs.getString(3)+ " x " + rs.getString(4)+ " x " + rs.getString(5));
            }
           
          } catch (SQLException e) {
            System.out.println("SQL Error!");
            e.printStackTrace();
          } finally {
            try {
              if (null != rs)
                rs.close();
              if (null != stmt)
                stmt.close();
            } catch (SQLException exp) {
              System.out.println("Unexpected SQL Error!");
              exp.printStackTrace();
            }
          }
        return result;   
    }

    public static void updateProd(Connection postGresConn, int ilgis, int plotis, int aukstis, int ID) throws SQLException {
        
        if (postGresConn == null) {
            System.out.println("We should never get here.");
        }
        
        Statement stmt = null;
        ResultSet rs = null;

        try {

            stmt = postGresConn.createStatement();

            stmt.executeUpdate(
                "UPDATE juba7706.produkcija " +
                "SET Ilgis = " + ilgis + ", "
                    +"Plotis = " + plotis + ", "
                    +"Aukstis = " + aukstis +" " +
                "WHERE ID = " + ID
            );     

        } catch (SQLException e) {
            System.out.println("SQL Error!");
            e.printStackTrace();
          } finally {
            try {
              if (null != rs)
                rs.close();
              if (null != stmt)
                stmt.close();
            } catch (SQLException exp) {
              System.out.println("Unexpected SQL Error!");
              exp.printStackTrace();
            }
          }
    }

    public static void deleteUzsak(Connection postGresConn, int ID) throws SQLException {
        
        if (postGresConn == null) {
            System.out.println("We should never get here.");
        }
        
        Statement stmt = null;
        ResultSet rs = null;

        try {

            stmt = postGresConn.createStatement();

            stmt.executeUpdate(
                "DELETE FROM juba7706.Produkcija "+
                "WHERE ID = " + ID
            );     

        } catch (SQLException e) {
            System.out.println("SQL Error!");
            e.printStackTrace();
          } finally {
            try {
              if (null != rs)
                rs.close();
              if (null != stmt)
                stmt.close();
            } catch (SQLException exp) {
              System.out.println("Unexpected SQL Error!");
              exp.printStackTrace();
            }
          }
    }

}