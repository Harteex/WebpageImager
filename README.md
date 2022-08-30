# WebpageImager
Displays websites as pictures

## What
WebpageImager runs a backend with a headless browser and has a simple website which updates the image in a certain interval. Every time the image is loaded from the backend, a new screenshot is taken.

## Why
This is nice solution for an old device to display a modern website it can't handle. WebpageImager doesn't provide any kind of interaction so it's viewing only, which is good enough for some uses cases, such as displaying a public transportation timetable.

## Get started
Configure the project in appsettings, launch it and open it up in a browser. The index html file uses the /Image endpoint to get an image.
Note that the ChromeDriver version must match your locally installed Chrome.
