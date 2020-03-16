# mars-rover

My first instinct was to build a windows service to pull/download any new dates from the "dates.txt" file. But with the introduction of cloud, I couldn’t guarantee a successful cross OS deploy. Then I thought about using the Azure WebJobs but again, a separate instance and cron scheduling would’ve created some new and unexpected issues.
The new date file load and download mechanism works inside the second webApi tear. The initiator is a secure pre-shared authorization key. “Posted” as a body payload to be inaccessible by any MIM attack.
A postman request is attached as a JSON import (mars-rover.postman_collection), to help with the call.

The site is already displaying any previously downloaded earth_date data. All downloaded images are also converted as 200x200 thumbnails to help with faster gallery loads. 

TODO:
Unit tests.

Docker files are available, but I’m working on running both sites through a single deploy file.     
