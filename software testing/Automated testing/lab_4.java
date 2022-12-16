package project;

import static org.junit.Assert.*;

import java.io.IOException;
import java.nio.charset.StandardCharsets;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.time.Duration;
import java.util.List;
import java.util.Random;
import java.util.function.Function;

import org.junit.After;
import org.junit.Before;
import org.junit.BeforeClass;
import org.junit.Test;
import org.openqa.selenium.By;
import org.openqa.selenium.TimeoutException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

public class TestIt {
	
	static class User{
		public static String Email;
		public static String Password;
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
	
	public static int generateInt(int min, int max) {
		return (int)(Math.random()*(max-min+1)+min);
	}
	
	static FirefoxDriver accountCreate;
	static FirefoxDriver driver;
	
	@BeforeClass
	public static void setSystem() {
		System.setProperty("webdriver.gecko.driver","C:\\Windows\\System32\\GroupPolicyUsers\\geckodriver.exe");
	}
	
	@BeforeClass
	public static void accountCreation() {
		accountCreate = new FirefoxDriver();
		accountCreate.get("https://demowebshop.tricentis.com/");
		accountCreate.manage().window().maximize();
		
		accountCreate.findElement(By.xpath("//*[contains(text(), 'Log in')]")).click();
		accountCreate.findElement(By.xpath("//input[@value='Register']")).click();
		
		accountCreate.findElement(By.xpath("//input[@id='gender-male']")).click();
		accountCreate.findElement(By.xpath("//input[@id='FirstName']")).sendKeys("Vardenis");
		accountCreate.findElement(By.xpath("//input[@id='LastName']")).sendKeys("Pavardenis");
		User.Email = generateString()+"@pastas.lt";
		accountCreate.findElement(By.xpath("//input[@id='Email']")).sendKeys(User.Email);
		User.Password = "Slaptazod1s!";
		accountCreate.findElement(By.xpath("//input[@id='Password']")).sendKeys("Slaptazod1s!");
		accountCreate.findElement(By.xpath("//input[@id='ConfirmPassword']")).sendKeys("Slaptazod1s!");
		accountCreate.findElement(By.xpath("//input[@value='Register']")).click();
		accountCreate.findElement(By.xpath("//input[@value='Continue']")).click();
		accountCreate.quit();
	}
	
	@Before
	public void driverInit() {
		driver = new FirefoxDriver();
	}
	
	@After
	public void driverQuit() {
		driver.quit();
		driver = null;
	}
	
	@Test
	public void test1() throws IOException, InterruptedException {
		driver.get("https://demowebshop.tricentis.com/");
		driver.manage().window().maximize();

		driver.findElement(By.xpath("//a[text()='Log in']")).click();
		driver.findElement(By.xpath("//input[@id='Email']")).sendKeys(User.Email);
		driver.findElement(By.xpath("//input[@id='Password']")).sendKeys(User.Password);
		driver.findElement(By.xpath("//input[@value=\"Log in\"]")).click();
		driver.findElement(By.xpath("//div[@class='listbox']//a[@href='/digital-downloads']")).click();
		
		WebDriverWait myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
		Path myPath = Paths.get("src\\test\\data1.txt");
		List<String> lines = Files.readAllLines(myPath, StandardCharsets.UTF_8);
		int i=0;
		for(String line : lines) {
			i++;
			final int number=i;
			final WebElement element2 = driver.findElement(By.xpath("//a[text()='%s']/following::input[@value='Add to cart']".formatted(line)));
			
			myWaitVar.until(new Function<WebDriver, Boolean>(){
				public Boolean apply(WebDriver driver) {
					
					
						if(driver.findElement(By.xpath("/descendant::span[@class='cart-qty']")).getText().equals("("+ Integer.toString(number) + ")")) {
							
							return true;
						}
						else {
							element2.click();
							return false;
						}  							
				     }
			});
		} 
		
		driver.findElement(By.xpath("//a[@href='/cart']")).click();
		driver.findElement(By.xpath("//input[@id='termsofservice']")).click();
		driver.findElement(By.xpath("//button[@id='checkout']")).click();
		driver.findElement(By.xpath("//select[@id='BillingNewAddress_CountryId']")).sendKeys("Lithuania");
		driver.findElement(By.xpath("//input[@id='BillingNewAddress_City']")).sendKeys(generateString());
		driver.findElement(By.xpath("//input[@id='BillingNewAddress_Address1']")).sendKeys(generateString());
		driver.findElement(By.xpath("//input[@id='BillingNewAddress_ZipPostalCode']")).sendKeys(Integer.toString(generateInt(10000, 99999)));
		driver.findElement(By.xpath("//input[@id='BillingNewAddress_PhoneNumber']")).sendKeys(Integer.toString(generateInt(860000000, 870000000-1)));

		driver.findElement(By.xpath("/descendant::input[@value='Continue']")).click();
		
		myWaitVar.until(ExpectedConditions.elementToBeClickable(By.xpath("/descendant::input[@type='button' and ancestor::li/@id='opc-payment_method']")));
		driver.findElement(By.xpath("/descendant::input[@type='button' and ancestor::li/@id='opc-payment_method']")).click();

		myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
		myWaitVar.until(ExpectedConditions.elementToBeClickable(By.xpath("/descendant::input[@type='button' and ancestor::li/@id='opc-payment_info']")));
		driver.findElement(By.xpath("/descendant::input[@type='button' and ancestor::li/@id='opc-payment_info']")).click();
		
		myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
		myWaitVar.until(ExpectedConditions.elementToBeClickable(By.xpath("/descendant::input[@value='Confirm']")));
		driver.findElement(By.xpath("/descendant::input[@value='Confirm']")).click();
		
		boolean passed = true;
		try {
			myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
			myWaitVar.until(ExpectedConditions.presenceOfElementLocated(By.xpath("//*[contains(text(), 'Your order has been successfully processed')]")));
		}
		catch(TimeoutException ex){
			passed = false;
		}
		
		assertTrue(passed);
	}
	
	@Test
	public void test2() throws IOException, InterruptedException {
		driver.get("https://demowebshop.tricentis.com/");
		driver.manage().window().maximize();

		driver.findElement(By.xpath("//a[text()='Log in']")).click();
		driver.findElement(By.xpath("//input[@id='Email']")).sendKeys(User.Email);
		driver.findElement(By.xpath("//input[@id='Password']")).sendKeys(User.Password);
		driver.findElement(By.xpath("//input[@value=\"Log in\"]")).click();
		driver.findElement(By.xpath("//div[@class='listbox']//a[@href='/digital-downloads']")).click();
		
		WebDriverWait myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
		Path myPath = Paths.get("src\\test\\data2.txt");
		List<String> lines = Files.readAllLines(myPath, StandardCharsets.UTF_8);
		int i=0;
		for(String line : lines) {
			i++;
			final int number=i;
			final WebElement element2 = driver.findElement(By.xpath("//a[text()='%s']/following::input[@value='Add to cart']".formatted(line)));
			
			myWaitVar.until(new Function<WebDriver, Boolean>(){
				public Boolean apply(WebDriver driver) {
					
					
						if(driver.findElement(By.xpath("/descendant::span[@class='cart-qty']")).getText().equals("("+ Integer.toString(number) + ")")) {
							
							return true;
						}
						else {
							element2.click();
							
							return false;
						}  							
				     }
			});
		} 
		
		driver.findElement(By.xpath("//a[@href='/cart']")).click();
		driver.findElement(By.xpath("//input[@id='termsofservice']")).click();
		driver.findElement(By.xpath("//button[@id='checkout']")).click();
		
		driver.findElement(By.xpath("/descendant::input[@value='Continue']")).click();
		
		myWaitVar.until(ExpectedConditions.elementToBeClickable(By.xpath("/descendant::input[@type='button' and ancestor::li/@id='opc-payment_method']")));
		driver.findElement(By.xpath("/descendant::input[@type='button' and ancestor::li/@id='opc-payment_method']")).click();

		myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
		myWaitVar.until(ExpectedConditions.elementToBeClickable(By.xpath("/descendant::input[@type='button' and ancestor::li/@id='opc-payment_info']")));
		driver.findElement(By.xpath("/descendant::input[@type='button' and ancestor::li/@id='opc-payment_info']")).click();
		
		myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
		myWaitVar.until(ExpectedConditions.elementToBeClickable(By.xpath("/descendant::input[@value='Confirm']")));
		driver.findElement(By.xpath("/descendant::input[@value='Confirm']")).click();
		
		boolean passed = true;
		try {
			myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
			myWaitVar.until(ExpectedConditions.presenceOfElementLocated(By.xpath("//*[contains(text(), 'Your order has been successfully processed')]")));
		}
		catch(TimeoutException ex){
			passed = false;
		}
		
		assertTrue(passed);
	}

}
