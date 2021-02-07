# Snapchat.MemoriesDownloader
This program allows you to download all of your Snapchat memories.  
Your data will download organised with the following file structure:  
```
- OUTPUT DIRECTORY NAME
-- YEAR 
--- MONTH 
---- DAY 
----- {date}.jpg
----- {date}.mp4
```

For example:

```
- OUTPUT
-- 2018 
--- 06 (June)
---- 02 (Saturday)
----- 2018-06-02 20:47:45 UTC.jpg
----- 2018-06-02 20:48:49 UTC.mp4
---- 03 (Sunday)
----- 2018-06-03 20:47:45 UTC.jpg
-- 2019
--- 01 (January)
---- 01 (Monday)
----- 2019-01-01 20:47:45 UTC.mp4
```
# How to run
- Download your Snapchat data: https://support.snapchat.com/en-US/a/download-my-data  
- Extract the zip-file  
- Place all the scripts in this folder OR set the -f flag pointing to the memories_history.json file  
- To override the 'Output' directory name set the -o flag pointing to the directory you want to store the output  
- Run the Snapshat.MemoriesDownloader.exe   
- Example:  
```Snapchat.MemoriesDownloader.exe -h ./memories_history.json -o Output```

# Trouble Shooting
Make sure you get a fresh zip-file before running the script, links will expire over time

# Running tests
To run the tests you will need to create a local-appsettings.json file and exchange {id} for your snapchat file ID.
```
{
    "TestMemoryId" : "{id}"
}
```
