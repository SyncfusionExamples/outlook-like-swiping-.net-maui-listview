# Achieve Outlook-Like Swiping Using .NET MAUI ListView
This repository controls the project that explains how to achieve Outlook-like swiping using the Syncfusion .NET MAUI ListView control.

## Blog reference
[Achieve Outlook-Like Swiping Using .NET MAUI ListView](https://www.syncfusion.com/blogs/post/achieve-outlook-like-swiping-using-net-maui-listview.aspx)

## XAML 

<Grid x:Name="mainGrid">
    <Grid.RowDefinitions>
        <RowDefinition Height="40" />
        <RowDefinition Height="*" />
    </Grid.RowDefinitions>
    <Label Grid.Row="0"
            Text="Inbox"
            TextColor="Black"
            FontSize="18"
            FontFamily="Roboto-Medium"
            Margin="16,0,0,0"
            VerticalOptions="Center" />
    <ListView:SfListView Grid.Row="1"
                            x:Name="listView"
                            ItemsSource="{Binding InboxInfos}"
                            AllowSwiping="True"
                            SwipeThreshold="100"
                            ItemSize="70"
                            SelectionMode="Multiple"
                            SelectionGesture="LongPress"
                            ScrollBarVisibility="Always"
                            AutoFitMode="Height">

        <ListView:SfListView.StartSwipeTemplate>
            <DataTemplate>
                <Grid BackgroundColor="#D8F3D4">
                    <Label Text="&#xe71C;"
                            FontFamily='{OnPlatform Android=ListViewFontIcons.ttf#,UWP=ListViewFontIcons.ttf#ListViewFontIcons,MacCatalyst=ListViewFontIcons,iOS=ListViewFontIcons}'
                            TextColor="Green"
                            HorizontalOptions="Center"
                            FontSize="22"
                            FontAttributes="Bold"
                            VerticalOptions="Center">
                    </Label>

                </Grid>
            </DataTemplate>
        </ListView:SfListView.StartSwipeTemplate>
        <ListView:SfListView.EndSwipeTemplate>
            <DataTemplate>
                <Grid BackgroundColor="#F4DEDE"
                        x:Name="listViewGrid">
                    <Label Text="&#xe716;"
                            FontFamily='{OnPlatform Android=ListViewFontIcons.ttf#,UWP=ListViewFontIcons.ttf#ListViewFontIcons,MacCatalyst=ListViewFontIcons,iOS=ListViewFontIcons}'
                            TextColor="DarkRed"
                            HorizontalOptions="Center"
                            FontSize="26"
                            VerticalOptions="Center">
                    </Label>
                </Grid>
            </DataTemplate>
        </ListView:SfListView.EndSwipeTemplate>

        <ListView:SfListView.GroupHeaderTemplate>
            <DataTemplate x:Name="GroupHeaderTemplate">
                <ViewCell>
                    <ViewCell.View>
                        <Grid HeightRequest="30">
                            <Label Text="{Binding Key, Converter={StaticResource groupHeaderTextConverter}}"
                                    TextColor="#666666"
                                    Margin="14, 7, 0, 0"
                                    FontSize="12"
                                    Grid.Row="1" />
                        </Grid>
                    </ViewCell.View>
                </ViewCell>
            </DataTemplate>
        </ListView:SfListView.GroupHeaderTemplate>

        <ListView:SfListView.ItemTemplate>
            <DataTemplate>
                <Grid>
                    <Grid.RowDefinitions>
                        <RowDefinition Height="5" />
                        <RowDefinition Height="20" />
                        <RowDefinition Height="20" />
                        <RowDefinition Height="20" />
                        <RowDefinition Height="5" />
                    </Grid.RowDefinitions>
                    <Grid.ColumnDefinitions>
                        <ColumnDefinition Width="72" />
                        <ColumnDefinition Width="*" />
                        <ColumnDefinition Width="Auto" />
                    </Grid.ColumnDefinitions>

                    <Grid Grid.Row="1"
                            Grid.Column="0"
                            Grid.RowSpan="2"
                            HeightRequest="35"
                            WidthRequest="35"
                            HorizontalOptions="Center"
                            VerticalOptions="Center">
                        <Image Source="{Binding Image}"
                                HeightRequest="40"
                                WidthRequest="40"
                                Margin="0, 15, 0, 0" />
                        <Label Text="{Binding ProfileName}"
                                TextColor="#FFFFFF"
                                FontSize="14"
                                HorizontalTextAlignment="Center"
                                HorizontalOptions="Center"
                                VerticalOptions="Center"
                                VerticalTextAlignment="Center"
                                FontFamily="Roboto-Regular"
                                CharacterSpacing="0.25"
                                Margin="0, 15, 0, 0" />
                    </Grid>

                    <Label Grid.Row="1"
                            Grid.Column="1"
                            Text="{Binding Name}"
                            FontFamily="Roboto-Medium"
                            FontSize="14"
                            FontAttributes="{Binding IsOpened, Converter={StaticResource fontAttributeConverter}}"
                            TextColor="#000000"
                            Margin="0, 2, 0, 0"
                            LineBreakMode="TailTruncation"
                            CharacterSpacing="0.25" />

                    <Label Grid.Row="2"
                            Grid.Column="1"
                            Grid.ColumnSpan="2"
                            Text="{Binding Subject}"
                            FontFamily="Roboto-Medium"
                            FontSize="13"
                            FontAttributes="{Binding IsOpened, Converter={StaticResource fontAttributeConverter}}"
                            Margin="0,0,25,3"
                            TextColor="#000000"
                            LineBreakMode="TailTruncation"
                            CharacterSpacing="0.25" />

                    <Label Grid.Row="3"
                            Grid.Column="1"
                            Grid.ColumnSpan="2"
                            Text="{Binding Description}"
                            FontFamily="Roboto-Regular"
                            FontSize="12"
                            TextColor="#666666"
                            Margin="0,0,16,1"
                            LineBreakMode="TailTruncation"
                            CharacterSpacing="0.25" />

                    <Label Grid.Row="1"
                            Grid.Column="2"
                            Text="{Binding Date, Converter={StaticResource dateTimeConverter}}"
                            TextColor="{Binding IsOpened, Converter={StaticResource textColorConverter}}"
                            FontFamily="Roboto-Regular"
                            FontAttributes="{Binding IsOpened, Converter={StaticResource fontAttributeConverter}}"
                            HorizontalOptions="End"
                            HorizontalTextAlignment="End"
                            FontSize="11"
                            Margin="0,0,16,0"
                            CharacterSpacing="0.20" />

                    <Image Grid.Row="2"
                            Grid.Column="2"
                            HeightRequest="30"
                            WidthRequest="30"
                            Margin="0, 0, 8, 0"
                            Source="paperclip.png"
                            IsVisible="{Binding IsAttached}"
                            HorizontalOptions="End"
                            VerticalOptions="Center">
                    </Image>

                    <Image Grid.Row="2"
                            Grid.Column="2"
                            HeightRequest="40"
                            WidthRequest="40"
                            Margin="0, 0, 2, 0"
                            Source="important.png"
                            IsVisible="{Binding IsImportant}"
                            HorizontalOptions="End"
                            VerticalOptions="Center">
                    </Image>
                </Grid>
            </DataTemplate>
        </ListView:SfListView.ItemTemplate>

    </ListView:SfListView>
    <Frame CornerRadius="4"
            Grid.Row="1"
            Margin="16,0,16,5"
            HeightRequest="40"
            VerticalOptions="End"
            IsVisible="{Binding IsDeleted}"
            Padding="0">
        <Grid HeightRequest="40"
                BackgroundColor="#3D454A">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="*" />
                <ColumnDefinition Width="Auto" />
            </Grid.ColumnDefinitions>
            <Label Grid.Column="0"
                    FontSize="14"
                    Text="{Binding PopUpText}"
                    TextColor="#FFFFFF"
                    VerticalOptions="Center"
                    VerticalTextAlignment="Center"
                    Margin="16,0,0,0"
                    CharacterSpacing="0.25" />
            <Label Grid.Column="1"
                    FontSize="14"
                    HorizontalOptions="End"
                    TextColor="#1796E6"
                    VerticalOptions="Center"
                    VerticalTextAlignment="Center"
                    Text="Undo"
                    Margin="0,0,16,0"
                    CharacterSpacing="0.25">
                <Label.GestureRecognizers>
                    <TapGestureRecognizer Command="{Binding UndoCommand}"/>
                </Label.GestureRecognizers>
            </Label>
        </Grid>
    </Frame>
</Grid>
## Requirements to run the demo

* [Visual Studio 2017](https://visualstudio.microsoft.com/downloads/) or [Visual Studio for Mac](https://visualstudio.microsoft.com/vs/mac/)
* Xamarin add-ons for Visual Studio (available via the Visual Studio installer).

## Troubleshooting

### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.