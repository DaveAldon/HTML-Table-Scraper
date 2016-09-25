#!/usr/bin/env python

import urllib2
from bs4 import BeautifulSoup

link = "http://www.cis.gvsu.edu/public/staffListing/index.php?page=staff&fname=Ira&lname=Woodring"

page = urllib2.urlopen(link)
soup = BeautifulSoup(page,"html.parser")
table = soup.find_all('table')
schedule_table = soup.find('table', id='ctl0_Main_tblSchedule')
rows = schedule_table.find_all('tr')
first = True
for row in rows:
    if first:
        first = False
        continue
    days = ['m', 't', 'w', 'r', 'f']
    index = 0
    cells = row.find_all('td')
    time = None
    for cell in cells:
        value = cell.text
        if index == 0:
            index = index + 1
            time = value
            continue
        if value.strip():
            print days[index - 1]," at ",time," you have ", value
        index = index + 1
    index = 0