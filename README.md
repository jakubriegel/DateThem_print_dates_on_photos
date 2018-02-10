# DateThem!

## about
DateThem! is a simple app for simple pourpose. It solves the lack of date printing ability in modern cameras and majority of software. Just select folder with photos, click *Date Them!* and it will do the rest.

![image](https://user-images.githubusercontent.com/32958017/36062458-61e8e056-0e6d-11e8-8a75-0fdb6ffd3834.png)

## implementation
After the user selects folder, the application using `Directory.GetFiles()` creates an array of paths to every `.jpg` file inside it. Then the *dating* logic is proceeded for every image.

The app uses properties stored with an image to get its date and dimentions, which are neccesary for positioning the date. In order to do that it creates an `Image` object from `FileStream` created from the path to the photo. Then using `PropertyItem` the properties are being retrieved. It also stores all the other properties, for adding them back to picture after dating.

Position of the date is being calculated using constant percentage value of picture dimensions, this way no matter of the image size on the computer screen it will be in the same place. Then the `ImageProccesor` comes in. `ImageFactory.Watermark()` prints the date on the photo. Then after original photo has been deleted, the one with date printed is being saved in the current path. It is worth mentioning, that before saving the image it receives all properties from the original one. Otherwise it would loose things like orientation, dimentions and date.   


## credits
<div>Icon made by <a href="https://www.flaticon.com/authors/smashicons" title="Smashicons">Smashicons</a> from <a href="https://www.flaticon.com/" title="Flaticon">www.flaticon.com</a> is licensed by <a href="http://creativecommons.org/licenses/by/3.0/" title="Creative Commons BY 3.0" target="_blank">CC 3.0 BY</a></div>
