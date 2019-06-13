""" 
    This file will investigate the use of beautiful soup in python. 
    bs4

    Beautiful Soup Documentation: 
    https://www.crummy.com/software/BeautifulSoup/bs4/doc/

    urllib3 is needed to open websites and make reqeuests. 
"""

from urllib.request import urlopen
from bs4 import BeautifulSoup


def main():
    page = urlopen("https://www.google.com")
    soup =  BeautifulSoup(page, "html.parser" ).encode('UTF-8')
    print (soup)

    return



if __name__ == "__main__":
    main()
