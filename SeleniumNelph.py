

from selenium import webdriver

from selenium.webdriver.common.keys import Keys

import time

import urllib as ul

from pathlib import Path
  
def main():
    """
        Doc
    """
  ##driver = webdriver.Chrome("chromedriver.exe")
  ##driver.get("http://www.python.org")
  ##res = driver.execute_script("return document.querySelector(\"title\").innerText")
  ##print(res)
  ##driver.close()

  ## theurl = "https://www.deviantart.com/yakovlev-vad/art/Big-present-800548429"
  ## wd = webdriver.Chrome(WebdriverBridge.DRIVERPATH)
  ## wd.get(theurl)
  ## cookies = wd.get_cookies()
  ## for stuff in wd.get_cookies():
  ##     print(stuff)
  ## dlbtn = wd.execute_script("return document.querySelector(\"a.dev-page-download\")")
  ## print("selected element: -----\n")
  ## print(dlbtn)
  ## dlbtn.click()
  ## dllink = wd.execute_script("return document.querySelector(\"a.dev-page-download\").href")
  ## time.sleep(10)
  ## wd.close()
  ## return
    theurl = "https://www.deviantart.com/yakovlev-vad/art/Pure-pony-786350260"
    wd = WebdriverBridge()
    wd.load(theurl)
    dlanchor = wd.select("a.dev-page-download")
    wd.inject_js("js.js")
    dlanchor.click()
    return

def main2():


    return

class WebdriverBridge:
    """
        A class that bridges some functionalities with selenium. 

        This class Seems to work properly, june 11 2019
    """
    DRIVERPATH = "chromedriver.exe"
    Instance = None
    SETTINGS = {"WBChromePath":"chromedriver.exe", "DownloadPath": "C:\\Users\\victo\\source\\repos\\Selenium_noobs\\"}

    def __init__(self):
        """
            Method should be called only once, we only want one instance of chrome. 
        """
        if(WebdriverBridge.Instance is not None):
            raise Exception("Instance Already Existed. You cannot instanciated 2 webdrivers.")
        chromeOptions = webdriver.ChromeOptions()
        prefs = {"download.default_directory" : WebdriverBridge.SETTINGS["DownloadPath"]}
        chromeOptions.add_experimental_option("prefs",prefs)
        driver = webdriver.Chrome(executable_path=WebdriverBridge.SETTINGS["WBChromePath"], 
            chrome_options=chromeOptions)
        self._Webdriver = driver
        WebdriverBridge.Instance = self
        return

    def load(self,url):

        self._Webdriver.get(url)
        return

    def get_driver(self):
        return self._Webdriver

    def js(self, jscommand):
        """
            Run JS on the webdriver.
            "E" will be returned if there is an execption. 
        """
        try:
            print("js command:\n")
            print(jscommand)
            stufftoreturn = self._Webdriver.execute_script(jscommand)
            print(stufftoreturn)
            return stufftoreturn
        except Exception as e:
            print(e)
            return "E"

    def select(self, selectstring, selectmode="css", multiple=False):
        """ 
        Select DOM object/s
        selectstring: The command
        selectmode: <css|id>
        multiple: A list of DOMs, or a DOM
        """
        if selectmode == "css":
            if multiple == True:
                return self.js("return document.querySelectorAll(\""+selectstring+"\")")
            return self.js("return document.querySelector(\""+selectstring+"\")")
        if selectmode == "id":
            return self._Webdriver.find_elements_by_id(selectstring)
        return

    def close(self):
        """
        Close WB and delete reference in the static class field. 
        """
        self._Webdriver.close()
        return
    
    # make a method that inject JS into the current page. 
    def inject_js(self, arg):
        """
            Given a string this method will try to run the js commands
            arg: a string. 
                If it's there exists a file when the given string is 
                viewed as path, then it will inject the content of the 
                file into the WB
            else it will just run the command as JS. 
        """
        if(Path(arg).is_file):
            with open(arg, "r") as f:
                injectcontent = f.read()
            return self._Webdriver.execute_script(injectcontent)
        return self._Webdriver.execute_script(arg)

def token_method():
    """
    Get the image using the token method
    """

    # The token gotten at oauth 2
    token = "ecef1f088dda9c9bf831e1f411a261d8572bef591c8fa02c64"
    # the webapp string gotten for the web we want to scrape, 
    # in content tag of the meta tag named da:appurl
    webappstr = "D5FE8B2B-B248-A930-942B-C998E9017E2C"


    return

if __name__ == "__main__":
    main()
