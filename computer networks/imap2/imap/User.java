public class User {
    private final String email;
    private final String pass;
    private final int port;
    private final String host;

    public User( String host, int port, String email, String pass) {
        this.email = email;
        this.pass = pass;
        this.port = port;
        this.host = host;
    }

    public String getHost() {
        return host;
    }

    public int getPort() {
        return port;
    }

    public String getPass() {
        return pass;
    }

    public String getEmail() {
        return email;
    }
}
