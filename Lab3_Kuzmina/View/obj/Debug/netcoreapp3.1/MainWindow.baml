   M S B A M L   `   `   `  ����?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5   	   D @ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullL4clr-namespace:ViewModel_namespace;assembly=ViewModelViewModel_namespace 5   	       View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:View     5   	   J	viewmodel4clr-namespace:ViewModel_namespace;assembly=ViewModel     5   	   -   5          � Title$  
MainWindow��5	   	   $	��700��6   $	��900��6)    � 	Resources 5         DataCollection �� Source  
DataOnGrid,  &ViewModel_namespace.DataItemConverter1  converterColl1,  &ViewModel_namespace.DataItemConverter2  converterColl2.  (ViewModel_namespace.DataOnGridConverter1  converterOnGrid1.  (ViewModel_namespace.DataOnGridConverter2  converterOnGrid2  collectionDataTemplate �� Orientation �� 	Converter��%�  &	        &	 4     &	 d     &	 w     &	 �     &	 �     &	 �     7  7  �� 5   
    61   �� *DataCollection+6S   �� 5   
    61   �� *
DataOnGrid+6O     5   
   6'     5   
   6'     5   
   6)     5   
   6)   �� 5   
   .R�5      �� $ Vertical=�6   .+�5      �� $%��В МОМЕНТ ВРЕМЕНИ:��6   �� 5      ��6   �� 8   �� 5      $$��Компоненты поля:��6   �� 5      ��6   �� 8  5      5      5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           View.MainWindow   AddCustomCommand   Command# Z 5      5   A   .��5       �  � ColumnDefinitions 5!   
   �� 5"      $��1* �6   �� 5#      $��1* �6   �� 5$      $��1* �6   �� 5%      $��1* �6   .+�5'   
   o� $��2��6   $��30��6#   $��10,10,10,10q�6/   $	��Top=�6D   ���	#FFFFF5CD.@�5(      m� -   5        $��commandsMenu1��5(      $
��File��6,   $��30��6:   $��50��6F   $��Stretch=�6Q   .@�5)      m� -   5        $	��New��5)      6(   m� 5*      $
��Open��6   	 m� Command	 6)   �� *OpenCommand+m� 5+      $
��Save��6   	 6)   �� *SaveCommand+5,      m� 5-      -   5        $��commandsMenu2��5-      $
��Edit��6,   $��50��6:   $��Stretch=�6E   $��Stretch=�6c   .@�5.      m� $��Add Defaults��6   	 61   �� *AddDefaultsCommand+m� 5/      $"��Add Default V1DataCollection��6   	 6A   �� *#!AddDefaultV1DataCollectionCommand+m� 50      $��Add Default V1DataOnGrid��6   	 6=   �� *AddDefaultV1DataOnGridCommand+m� 51      $��Add Element from File��6   	 6:   �� *AddElementFromFileCommand+m� 52      $��Remove��6   	 6+   �� *RemoveCommand+53      m� 54      -   5        $��commandsMenu3��54      $��Add Custom V1DataOnGrid��6,   #	 Z 6M   $��30��55      $	��170��6#   $��Stretch=�6/   56      �� 57   
   $
��Left=�6   $��	10,45,0,0q�6/   $��MainCollection��6B   $
��Wrap=�6X   $	��Top=�6l   $	��120��6�   ���	#FF720BB76�   �� 58   
   -   5        $��listBox_Main��58      $��0��6&   $��10,68,10,10q�66   ��6K   �� *MainCollection+
 �� SelectedItem
 59      �� *RemovedItem+ �� Mode$ OneWayToSource=� �� SelectedIndex 6L   �� *RemovedIndex+$ OneWayToSource=�� 5;   
   -   5        $��grid_AddCustom��5;      $��1��6%   $��2��65   $��0,40,0,0q�6I    5<      �� 5=      $��1* �6#   �� 5>      $��1* �6#    � RowDefinitions 5@      �� 5A      $��5* �6    �� 5B      $��1* �6    .+�5D      �� $��0��6   $��0��6(   $
��Left=�65   $��10,5,0,0q�6P   $��DataCollection��6b   $
��Wrap=�6x   $	��Top=�6�   $	��120��6�   ���	#FF720BB76�   �� 5E      -	   5        $��listBox_DataCollection��5E      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5F      $��0��66   $��0��6F   $��10,30,10,10q�6S   ��5E   4   ��  �� Source# [  5F   S   �� 5G      $��1��6   $��0��6(   $
��Left=�65   $��10,5,0,0q�6P   $��
DataOnGrid��6b   $
��Wrap=�6t   $	��Top=�6�   $	��120��6�   ���	#FF720BB76�   �� 5H      -
   5        $��listBox_DataOnGrid��5H      $
 TrueY�5I      $��1��66   $��0��6F   $��10,30,10,10q�6S   ��5H   0   �� # [ 5I   S   �� 5J      $��0��6   $��1��6(   $
��Left=�65   $��10,10,10,10q�6P   $��New V1DataOnGrid��6e   $
��Wrap=�6}   $	��Top=�6�   $	��120��6�   ���	#FF720BB7$��Italic$�6�   $��13(�6�   �� 5K      $��0��6   $��1��6(   $
��Left=�65   $��10,30,10,10q�6P   $��string��6e   $
��Wrap=�6s   $	��Top=�6�   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5L      $��0��6   $��1��6(   $
��Left=�65   $��10,65,10,10q�6P   $��number of nodes��6e   $
��Wrap=�6|   $	��Top=�6�   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5M      $��1��6   $��1��6(   $
��Left=�65   $��10,30,10,10q�6P   $��minValue��6e   $
��Wrap=�6u   $	��Top=�6�   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5N      $��1��6   $��1��6(   $
��Left=�65   $��10,65,10,10q�6P   $��maxValue��6e   $
��Wrap=�6u   $	��Top=�6�   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5O      $��0��6   $��1��6&   $
��Left=�63   $��10,45,10,10q�6N    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��180��6�   $��20��6�   ��5P      �� *Info+ �� ValidatesOnDataErrors$
 True�� �� UpdateSourceTrigger$ PropertyChanged=�$ TwoWay=��� 5Q      $��0��6   $��1��6&   $
��Left=�63   $��10,80,10,10q�6N   $
 Wrap=�6c   $	��Top=�6w   $	��120��6�   $��20��6�   ��5R      �� *NodesNumber+$
 True��$ PropertyChanged=�$ TwoWay=��� 5S      $��1��6   $��1��6&   $
��Left=�63   $��10,45,10,10q�6N   $
 Wrap=�6c   $	��Top=�6w   $	��120��6�   $��20��6�   ��5T      �� *
MinValue+$
 True��$ TwoWay=��� 5U      $��1��6   $��1��6&   $
��Left=�63   $��10,80,10,10q�6N   $
 Wrap=�6c   $	��Top=�6w   $	��120��6�   $��20��6�   ��5V      �� *
MaxValue+$
 True��$ TwoWay=�5W      � 5Y   
   -   5        $��grid3��5Y      $��3��6    5Z      �� 5[      $��1* �6    �� 5\      $��2* �6    �� 5]      $��1* �6    �� 5^      $��1* �6    .+�5`      �� $��0��6   $
��Left=�6%   $��	10,80,0,0q�6@   $��maxNumberOfTests��6S   $
��Wrap=�6k   $	��Top=�6   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5a      $��0��6   $��
10,100,0,0q�6%   $
��Wrap=�6[   $
��Left=�6o   $	��Top=�6�   $	��185��6�   $��20��6�   ��69   �� *maxNumberOfTests+6�   �� 5b      $��1��6   $
��Left=�6%   $��
10,0,10,10q�6@   $��Details��6T   $
��Wrap=�6c   $	��Top=�6w   $��88��6�   ���	#FF720BB7$��Italic$�6�   $��11(�6�   �� 5c      -   5        $��listBox_details��5c      $��1��6-   $��0,20,5,0q�6:   #��[ 5e      ��5d      �� # [   �� Path$ 	DataItems�5e      �� 5f      $��2��6   $��5,5,0,0q�6%   $
��Left=�66   $
��Wrap=�6Q   $	��Top=�6e   $	��107��6}   $	��210��6�   ��5g      ��  �� ElementName$ listBox_DataOnGrid��$ SelectedItem�# [ �� 5h      $��3��6   $��5,5,0,0q�6%   $
��Left=�66   $
��Wrap=�6Q   $	��Top=�6e   $	��107��6}   $	��210��6�   ��5i      �� $ listBox_DataOnGrid��$ SelectedItem�# [ 5j      �� 5k   
   -   5        $��commandsPanel��5k      $ 
Horizontal=�6*   $��
0,10,0,623q�6C   $��2��6W   .+�5l      �� $
��Open��6   $��2��5m      $
��Left=�6%   $��10,5,0,0q�6@   $	��Top=�6R   ��5l   $   �� *OpenCommand+5m   R   �� 5n      $
��Save��6   $��2��5o      $
��Left=�6%   $��10,5,0,0q�6@   $	��Top=�6R   ��5n   $   �� *SaveCommand+5o   R   �� 5p      $��
Add Custom��6   #��Z 6*   $��2��5q      $
��Left=�6%   $��10,5,0,0q�6@   $	��Top=�6R   �� 5r      $��Remove��6   $��2��5s      $
��Left=�6%   $��10,5,0,0q�6@   $	��Top=�6R   $��2��6j   ��5r   &   �� *RemoveCommand+5s   j   5t      5u      5v      