public class A {
    B b1 = new B();
    public A(){System.out.print("A");}
    static B b2 = new B();
    public static void main(String args[]){new A();}
    static A a = new A();
  }
  class B {
    public B(){System.out.print("B");}
  }