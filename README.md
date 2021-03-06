# StrapUp.Forms
Free Open source Xamarin Forms Controls Library. 

<img src="/TestApp/TestApp/Images/StrapUpLogo/StrapUpLogoBrand.png" alt="Logo" width="250"/>


Controls:
- Card View
- Expansion Panel. 

Stay tuned...more controls to come (Windows Live Tile and alot of the material design components https://material.io/guidelines/)

To Install
------------
Install the nuget package in each of your projects:

`Install-Package StrapUp.Forms.Controls`

You also need to install iconize for the FontAwesome icons:
https://github.com/jsmarcus/Xamarin.Plugins/tree/master/Iconize

For iOS:
To use these controls on iOS goto the AppDelegate.cs and add the following code before LoadApplication:
`StrapUp.Forms.Controls.Init.Controls();`

I have tested targeting Profile 111 for iOS, Android, UWP, and Windows 8.1 apps.



Card View
----------

iOS                  |  Android    |     UWP
:-------------------------:|:-------------------------:|:-------------------------:
<img src="/TestApp/TestApp/Images/CardViewIOS.png" alt="Logo" width="250"/> | <img src="/TestApp/TestApp/Images/CardviewAndroid.png" alt="Logo" width="255"/> | <img src="/TestApp/TestApp/Images/CardViewUWP.png" alt="Logo" width="300"/> 

```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:strapup="clr-namespace:StrapUp.Forms.Controls;assembly=StrapUp.Forms"
		     xmlns:iconize="clr-namespace:FormsPlugin.Iconize;assembly=FormsPlugin.Iconize"
             x:Class="TestApp.CardViewPage"
             Padding="20,20,20,20">
    <StackLayout>

		<strapup:CardView Source="https://www.smashingmagazine.com/images/nature-wallpapers/3.jpg"
				AnimateCardClick="true">
			<strapup:CardView.Stack>
				<StackLayout Orientation="Horizontal">
					<Label TextColor="#9E9E9E" HorizontalOptions="StartAndExpand">
					CardView Stacklayout Content</Label>
                    <iconize:IconLabel Text="fa-ellipsis-v" 
                                       FontFamily="FontAwesome" 
                                       FontSize="24" 
                                       TextColor="#9E9E9E"></iconize:IconLabel>
				</StackLayout>
			</strapup:CardView.Stack>
		</strapup:CardView>
			
    </StackLayout>
</ContentPage>
```

Expansion Panel
----------------
iOS                  |  Android    |     UWP
:-------------------------:|:-------------------------:|:-------------------------:
<img src="/TestApp/TestApp/Images/ExpansionPanelIOS.png" alt="Logo" width="250"/> | <img src="/TestApp/TestApp/Images/ExpansionPanelAndroid.png" alt="Logo" width="250"/> | <img src="/TestApp/TestApp/Images/ExpansionPanelUWP.png" alt="Logo" width="250"/> 


```xaml
<?xml version="1.0" encoding="utf-8" ?>
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:strapup="clr-namespace:StrapUp.Forms.Controls;assembly=StrapUp.Forms"
             xmlns:local="clr-namespace:TestApp;assembly=TestApp"
             xmlns:xreference="clr-namespace:Xamarin.Forms.Xaml"
             x:Class="TestApp.ExpansionPanelPage" 
             BackgroundColor="Gray"
             Padding="5,5,5,5">
    <ScrollView>
        <StackLayout x:Name="SL"  HorizontalOptions="FillAndExpand" 
                     VerticalOptions="FillAndExpand" >
            
			<strapup:ExpansionPanel BackgroundColor="White" HideIcon="False" Command="{Binding PassedInMethodCommand}" >
                <strapup:ExpansionPanel.CollapsedPanel>
                    <StackLayout>
                        <Label Text="{Binding CollapsedText}" 
                            FontSize="Large" 
                            FontAttributes="Bold"></Label>
                    </StackLayout>
                </strapup:ExpansionPanel.CollapsedPanel>
                <strapup:ExpansionPanel.ExpandedPanel>
                    <StackLayout>
                        <Entry Text="{Binding IsLoading, Mode=TwoWay}"></Entry>
                        <Label Text="Mote Stuff"></Label>
                        <ListView ItemsSource="{Binding MyItems, Mode=TwoWay}" HasUnevenRows="True">
                            <ListView.ItemTemplate>
                                <DataTemplate>
                                    <ViewCell>
                                        <StackLayout>
                                            <Label Text="{Binding text}"></Label>
                                            <Label Text="{Binding details}"></Label>
                                        </StackLayout>
                                    </ViewCell>
                                </DataTemplate>
                            </ListView.ItemTemplate>
                        </ListView>
                    </StackLayout>
                </strapup:ExpansionPanel.ExpandedPanel>
            </strapup:ExpansionPanel>


        </StackLayout>
    </ScrollView>
</ContentPage>
```
