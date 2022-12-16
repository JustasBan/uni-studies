package testing;

import java.time.Duration;
import java.util.Random;
import java.util.function.Function;

import org.openqa.selenium.By;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

public class lab3_2 {

	public static void main(String args[]) {
		System.out.println("START");
		
		System.setProperty("webdriver.gecko.driver","C:\\Windows\\System32\\GroupPolicyUsers\\geckodriver.exe");
		WebDriver driver = new FirefoxDriver();
		driver.get("https://demoqa.com/");
		driver.manage().window().maximize();
		
		driver.findElement(By.xpath("/descendant::div[*/h5[text() = 'Elements']]")).click();
		
		WebElement element = driver.findElement(By.xpath("/descendant::li[span[text()='Web Tables']]"));
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", element);
		element.click();
		
		Function<WebDriver, Boolean> func = new Function<WebDriver, Boolean>(){
			public Boolean apply(WebDriver driver) {
				driver.findElement(By.xpath("/descendant::button[@id = 'addNewRecordButton']")).click();
				driver.findElement(By.xpath("/descendant::input[@id = 'firstName']")).sendKeys(generateString());
				driver.findElement(By.xpath("/descendant::input[@id = 'lastName']")).sendKeys(generateString());
				driver.findElement(By.xpath("/descendant::input[@id = 'userEmail']")).sendKeys(generateString()+"@pastas.lt");
				driver.findElement(By.xpath("/descendant::input[@id = 'age']")).sendKeys(Integer.toString(generateInt(18, 99)));
				driver.findElement(By.xpath("/descendant::input[@id = 'salary']")).sendKeys(Integer.toString(generateInt(900, 9000)));
				driver.findElement(By.xpath("/descendant::input[@id = 'department']")).sendKeys(generateString());
				driver.findElement(By.xpath("/descendant::button[@id = 'submit']")).click();

				if(driver.findElement(By.xpath("/descendant::span[@class = '-totalPages']")).getText().equals("2")) {
					return true;
				}
				else {
					return false;
				}
			}
		};
		
		WebDriverWait myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
		myWaitVar.until(func);

		WebElement element2 = driver.findElement(By.xpath("/descendant::button[text() = 'Next']"));
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", element2);
		
		driver.findElement(By.xpath("/descendant::button[text() = 'Next']")).click();
		driver.findElement(By.xpath("/descendant::span[@title = 'Delete']")).click();
		WebElement element3 = driver.findElement(By.xpath("/descendant::input[@aria-label = 'jump to page']"));
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", element3);
		
//		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", element3);

		try {
			driver.findElement(By.xpath("/descendant::input[@aria-label = 'jump to page' and @value='1']"));
			driver.findElement(By.xpath("/descendant::span[@class='-totalPages' and text()='1']"));
			
			System.out.println("PASSED");
		}
		catch (NoSuchElementException ex){

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
	
	public static int generateInt(int min, int max) {
		return (int)(Math.random()*(max-min+1)+min);
	}
}
