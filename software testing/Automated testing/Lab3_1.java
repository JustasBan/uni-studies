package testing;

import java.time.Duration;
import java.util.concurrent.TimeUnit;

import org.openqa.selenium.By;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebDriver;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.firefox.FirefoxDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

public class Lab3_1 {
	public static void main(String args[]) {
		
		System.out.println("START");
		
		System.setProperty("webdriver.gecko.driver","C:\\Windows\\System32\\GroupPolicyUsers\\geckodriver.exe");
		WebDriver driver = new FirefoxDriver();
		driver.get("https://demoqa.com/");
		driver.manage().window().maximize();
			
		driver.findElement(By.xpath("/descendant::div[*/h5[text() = 'Widgets']]")).click();
		
		WebElement element = driver.findElement(By.xpath("/descendant::li[span[text()='Progress Bar']]"));
		((JavascriptExecutor) driver).executeScript("arguments[0].scrollIntoView(true);", element);
		

		driver.findElement(By.xpath("/descendant::li[span[text()='Progress Bar']]")).click();
		driver.findElement(By.xpath("/descendant::button[text() = 'Start']")).click();
				
		try {
			WebDriverWait myWaitVar = new WebDriverWait(driver, Duration.ofSeconds(30));
			myWaitVar.until(ExpectedConditions.presenceOfElementLocated(By.xpath("/descendant::div[text() = '100%' and @class='progress-bar bg-success']")));
		}
		catch(Exception ex) {
			driver.close();
			System.out.println("FAIL");
		}

		driver.findElement(By.xpath("/descendant::button[@id = 'resetButton']")).click();
		
		try {
			driver.findElement(By.xpath("/descendant::div[text() = '0%' and @class='progress-bar bg-info']"));
			
			System.out.println("PASSED");
		}
		catch (NoSuchElementException ex){

			System.out.println("FAILED");
		}
		
		driver.quit();
	}
}
