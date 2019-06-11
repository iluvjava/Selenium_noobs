





from selenium import webdriver

from selenium.webdriver.common.keys import Keys
  
def main():
    """
        Doc
    """
    driver = webdriver.Chrome("chromedriver.exe")
    driver.get("http://www.python.org")
    res = driver.execute_script("return document.querySelector(\"title\").innerText")
    print(res)
    driver.close()
    return

if __name__ == "__main__":
    main()
