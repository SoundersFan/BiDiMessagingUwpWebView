# Bidirectional Communications between UWP <==> WebView

## What is this?
This is a sample that shows how a UWP app can host a WebView with content that can communicate with the hosting UWP app and vise-versa. 

## Build
* Install Visual Studio 2019 with UWP workload
* `git clone https://github.com/SoundersFan/BiDiMessagingUwpWebView`
* `cd BiDiMessagingUwpWebView`
* `start BiDiMessagingUwpWebView.sln`
* Press F5

## WebView to UWP:
To communicate from the WebView content to the UWP app, the UWP app needs to be setup to do this an a perscribed way with a named object; "host" in my case. Set up the app:
1. In the XAML, handle the [WebView.NavigationStarting](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.webview.navigationstarting) ([link to code]()) event and in it call the [WebView.AddWebAllowedObject](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.webview.addweballowedobject) ([link to code]()) passing in a name (I use "host") and the Universal Windows Component to call into.
2. Add a Universal Windows Component to call into([link to code]()). **Important**: This component class needs to be in a separate Universal Windows Component library, separate from the App's project. It also must be tagged with the [AllowForWeb](https://docs.microsoft.com/en-us/uwp/api/windows.foundation.metadata.allowforwebattribute) attribute ([link to code]()).
3. Add the [ApplicationContentUriRules](https://docs.microsoft.com/en-us/uwp/schemas/appxpackage/uapmanifestschema/element-uap-applicationcontenturirules) to the Package.appxmanifest ([link to code]()).
4. In the JavaScript call, [window._host_.incrementHostCounter](). In this sample that fires an event that the MainPage is listeing to which updates the counter in the UWP side.

## UWP to WebView
To communicate from the UWP app to the WebView content, it's best for the content's JavaScript to have functions that receive the message.
1. Add methods to the page's JavaScript, such as [incrementWebCounter()](), to receive the message.
2. Call the [WebView.InvokeScriptAsync](https://docs.microsoft.com/en-us/uwp/api/windows.ui.xaml.controls.webview.invokescriptasync)("eval", new [] { "incrementWebCounter()" }) to send the message. Parameters can be added with in the parentheses to data.

## Extras
In the final version of the code there is another set of BiDi communications that sends data (a string to be specific) in both directions.

![Screen capture of the application. WebView on top hald and UWP XAML content on the bottom. Both contents have a button that when pressed increment their own counters and send messages to the other to increment the others. There are also buttons and text inputs that do a similar thing but with message strings.](ScreenShot.png)
