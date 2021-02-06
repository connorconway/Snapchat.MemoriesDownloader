# Snapchat.MemoriesDownloader
This program allows you to download all of your Snapchat memories.  
Your data will download organised with the following file structure:  
```
- YEAR 
-- MONTH 
--- DAY 
---- {date}.jpg
---- {date}.mp4
```

For example:

```
- 2018 
-- 06 (June)
--- 02 (Wednesday)
---- 2018-06-02.jpg
---- 2018-06-02.mp4
--- 03 (Thursday)
---- 2018-06-03.jpg
- 2019
```
# How to run
- Download your Snapchat data: https://support.snapchat.com/en-US/a/download-my-data  
- Extract the zip-file  
- Place all the scripts in this folder OR set the -f flag pointing to the memories_history.json file  
- Run the Snapshat.MemoriesDownloader.exe   
- Example:  
```Snapshat.MemoriesDownloader.exe -h ./memories_history.json```

# Trouble Shooting
Make sure you get a fresh zip-file before running the script, links will expire over time
