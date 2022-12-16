package testing;

import java.util.Random;
import java.util.concurrent.TimeUnit;

import org.openqa.selenium.By;
import org.openqa.selenium.firefox.FirefoxDriver;

public class Lab2 {
	public static void main(String args[]) throws InterruptedException {
		System.out.println("START");
		System.setProperty("webdriver.gecko.driver","C:\\Windows\\System32\\GroupPolicyUsers\\geckodriver.exe");
		FirefoxDriver driver = new FirefoxDriver();
		driver.get("https://demowebshop.tricentis.com/");
		driver.manage().window().maximize();
		
		driver.findElement(By.xpath("//*[contains(text(), 'Log in')]")).click();
		driver.findElement(By.xpath("//input[@value='Register']")).click();
		
		driver.findElement(By.xpath("//input[@id='gender-male']")).click();
		driver.findElement(By.xpath("//input[@id='FirstName']")).sendKeys("Vardenis");
		driver.findElement(By.xpath("//input[@id='LastName']")).sendKeys("Pavardenis");
		driver.findElement(By.xpath("//input[@id='Email']")).sendKeys(generateString()+"@pastas.lt");
		driver.findElement(By.xpath("//input[@id='Password']")).sendKeys("Slaptazod1s!");
		driver.findElement(By.xpath("//input[@id='ConfirmPassword']")).sendKeys("Slaptazod1s!");
		driver.findElement(By.xpath("//input[@value='Register']")).click();
		driver.findElement(By.xpath("//input[@value='Continue']")).click();
		
		driver.findElement(By.xpath("//div[@class='listbox']//a[@href='/computers']")).click();
		driver.findElement(By.xpath("//div[@class='picture']//a[@href='/desktops']")).click();
		driver.findElement(By.xpath("/descendant::input[@value= 'Add to cart' and ./preceding::span[@class='price actual-price']/text() > '1500.00']")).click();
		
		driver.findElement(By.xpath("//input[@id='add-to-cart-button-74']")).click();
		driver.findElement(By.xpath("//*[contains(text(), 'Shopping cart')]")).click();
		driver.findElement(By.xpath("//input[@name='removefromcart']")).click();
		
		driver.findElement(By.xpath("//input[@value='Update shopping cart']")).click();
		

		if(driver.findElement(By.xpath("//*[contains(text(), 'Your Shopping Cart is empty!')]")).isDisplayed()) {
			System.out.println("PASSED");
		}
		else {
			System.out.println("FAILED");
		}
		driver.quit();
	}
	
	public static String generateString() {
		int leftLimit = 97; // letter 'a'
	    int rightLimit = 122; // letter 'z'
	    int targetStringLength = 10;
	    Random random = new Random();
	    StringBuilder buffer = new StringBuilder(targetStringLength);
	    for (int i = 0; i < targetStringLength; i++) {
	        int randomLimitedInt = leftLimit + (int) 
	          (random.nextFloat() * (rightLimit - leftLimit + 1));
	        buffer.append((char) randomLimitedInt);
	    }
	    return buffer.toString();
	}
}
