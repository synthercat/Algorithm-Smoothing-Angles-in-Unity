# Algorithm: Smoothing out 0-360 degree readouts from a sensor

### Description
This was an algorithm I had to come up to in order to use it for my [RealSunAR](http://bit.ly/2TfJY6w) Unity plugin update (Version 2.00).

### The problem
When you try to get average between 2 angles, one valued `355` and one valued `5` instead of getting a result of `0` it would output `(355 + 5) / 2 = 180`

### The solution

We need to swift/offset the majority of the values to the `180` part of a circle

1. Divide the circle into 8 90-degree overlapping sections and count how many values we got in each one
2. Offset the values according to where most values are found under step 1
3. Sort the values in order to remove peak values to get rid of false peak readings
4. Remove the highest and lowest 20% of the values
5. Find the average of the remaining values
6. Undo the offset we did in step 2

### Usage

Just copy all files into your **Assets** folder a new project (I created this using Unity 2018.4.0 but should work in all versions). Go to the `Assets/Angle Smoothing` folder and open the `Angle Smoothing` scene

Press play!

![img](https://github.com/synthercat/Algorithm-Smoothing-Angles-in-Unity/blob/master/Screenshot.png)

* As you see I already have sample values in the `Angle Smoothing` gameobject under it's tab named "Original"
* The Game window shows a graphic representation of the process
* The red values represent the original values
* The green+blue values represent the offset values
* The blue are the remaining after the removal of peak values
* Finaly the white shows the solution after it's been swifted back to it's place
* You can see the results in the console and also the mid steps on the inspector of Angle Smoothing as indexes!

