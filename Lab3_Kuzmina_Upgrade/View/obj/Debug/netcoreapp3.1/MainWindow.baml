   M S B A M L   `   `   `  ����?  ;View, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:ViewView  5   	   D @ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullL4clr-namespace:ViewModel_namespace;assembly=ViewModelViewModel_namespace 5   	       View.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:View     5   	   J	viewmodel4clr-namespace:ViewModel_namespace;assembly=ViewModel     5   	   -   5          � Title$  
MainWindow��5	   	   $	��700��6   $	��900��6)    � 	Resources 5         MainCollection �� Source  DataCollection  
DataOnGrid,  &ViewModel_namespace.DataItemConverter1  converterColl1,  &ViewModel_namespace.DataItemConverter2  converterColl2.  (ViewModel_namespace.DataOnGridConverter1  converterOnGrid1.  (ViewModel_namespace.DataOnGridConverter2  converterOnGrid2  collectionDataTemplate �� Orientation �� 	Converter��%&  &	        &	 !     &	 T     &	 �     &	 �     &	 �     &	 �     &	 �     7  7  �� 5   
    61   �� 6D   �� 5   
   -   5         5   1   �� 6[   �� 5   
   -   5         5   1   �� 6W     5   
   6'     5   
   6'     5   
   6)     5   
   6)   �� 5   
   .R�5      �� $ Vertical=�6   .+�5      �� $%��В МОМЕНТ ВРЕМЕНИ:��6   �� 5      ��6   �� 8   �� 5      $$��Компоненты поля:��6   �� 5      ��6   �� 8  5      5      5       J� CommandBindings 5      )  #System.Windows.Input.CommandBinding  5   
   -   5           Command$  ApplicationCommands.Delete��5      5   >     5    
   -   5           View.MainWindow   AddCustomCommand# Z 5       5!   A   .��5#      �  � ColumnDefinitions 5$   
   �� 5%      $��1* �6   �� 5&      $��1* �6   �� 5'      $��1* �6   �� 5(      $��1* �6   .+�5*   
   o� $��2��6   $��30��6#   $��10,10,10,10q�6/   $	��Top=�6D   ���	#FFFFF5CD.@�5+      m� -   5        $��commandsMenu1��5+      $
��File��6,   $��30��6:   $��50��6F   $��Stretch=�6Q   .@�5,      m� -   5        $	��New��5,      6(   m� 5-      $
��Open��6   	 m� Command	 6)   �� *OpenCommand+m� 5.      $
��Save��6   	 6)   �� *SaveCommand+5/      m� 50      -   5        $��commandsMenu2��50      $
��Edit��6,   $��50��6:   $��Stretch=�6E   $��Stretch=�6c   .@�51      m� $��Add Defaults��6   	 61   �� *AddDefaultsCommand+m� 52      $"��Add Default V1DataCollection��6   	 6A   �� *#!AddDefaultV1DataCollectionCommand+m� 53      $��Add Default V1DataOnGrid��6   	 6=   �� *AddDefaultV1DataOnGridCommand+m� 54      $��Add Element from File��6   	 6:   �� *AddElementFromFileCommand+m� 55      $��Remove��6   $ 	 ApplicationCommands.Delete��6+   56      m� 57      -	   5        $��commandsMenu3��57      $��Add Custom V1DataOnGrid��6,   #	 Z 6M   $��30��58      $	��170��6#   $��Stretch=�6/   59      �� 5:   
   $
��Left=�6   $��	10,45,0,0q�6/   $��MainCollection��6B   $
��Wrap=�6X   $	��Top=�6l   $	��120��6�   ���	#FF720BB76�   �� 5;   
   -
   5        $��listBox_Main��5;      $��0��5<      $��10,68,10,10q�6   ��62   �� 
 �� Source#
 [  � 5>   
   -   5        $��grid_AddCustom��5>      $��1��6%   $��2��65   $��0,40,0,0q�6I    5?      �� 5@      $��1* �6#   �� 5A      $��1* �6#    � RowDefinitions 5C      �� 5D      $��5* �6    �� 5E      $��1* �6    .+�5G      �� $��0��6   $��0��6(   $
��Left=�65   $��10,5,0,0q�6P   $��DataCollection��6b   $
��Wrap=�6x   $	��Top=�6�   $	��120��6�   ���	#FF720BB76�   �� 5H      -   5        $��listBox_DataCollection��5H      $ �� IsSynchronizedWithCurrentItem$
 TrueY�5I      $��0��66   $��0��6F   $��10,30,10,10q�6S   ��5H   4   �� #
 [ 5I   S   �� 5J      $��1��6   $��0��6(   $
��Left=�65   $��10,5,0,0q�6P   $��
DataOnGrid��6b   $
��Wrap=�6t   $	��Top=�6�   $	��120��6�   ���	#FF720BB76�   �� 5K      -   5        $��listBox_DataOnGrid��5K      $
 TrueY�5L      $��1��66   $��0��6F   $��10,30,10,10q�6S   ��5K   0   �� #
 [ 5L   S   �� 5M      $��0��6   $��1��6(   $
��Left=�65   $��10,10,10,10q�6P   $��New V1DataOnGrid��6e   $
��Wrap=�6}   $	��Top=�6�   $	��120��6�   ���	#FF720BB7$��Italic$�6�   $��13(�6�   �� 5N      $��0��6   $��1��6(   $
��Left=�65   $��10,30,10,10q�6P   $��string��6e   $
��Wrap=�6s   $	��Top=�6�   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5O      $��0��6   $��1��6(   $
��Left=�65   $��10,65,10,10q�6P   $��number of nodes��6e   $
��Wrap=�6|   $	��Top=�6�   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5P      $��1��6   $��1��6(   $
��Left=�65   $��10,30,10,10q�6P   $��minValue��6e   $
��Wrap=�6u   $	��Top=�6�   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5Q      $��1��6   $��1��6(   $
��Left=�65   $��10,65,10,10q�6P   $��maxValue��6e   $
��Wrap=�6u   $	��Top=�6�   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5R      $��0��6   $��1��6&   $
��Left=�63   $��10,45,10,10q�6N    �� TextWrapping$
 Wrap=�6c   $	��Top=�6w   $	��180��6�   $��20��6�   ��5S      �� *Info+ �� ValidatesOnDataErrors$
 True�� �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=��� 5T      $��0��6   $��1��6&   $
��Left=�63   $��10,80,10,10q�6N   $
 Wrap=�6c   $	��Top=�6w   $	��120��6�   $��20��6�   ��5U      �� *NodesNumber+$
 True��$ PropertyChanged=�$ TwoWay=��� 5V      $��1��6   $��1��6&   $
��Left=�63   $��10,45,10,10q�6N   $
 Wrap=�6c   $	��Top=�6w   $	��120��6�   $��20��6�   ��5W      �� *
MinValue+$
 True��$ TwoWay=��� 5X      $��1��6   $��1��6&   $
��Left=�63   $��10,80,10,10q�6N   $
 Wrap=�6c   $	��Top=�6w   $	��120��6�   $��20��6�   ��5Y      �� *
MaxValue+$
 True��$ TwoWay=�5Z      � 5\   
   $��3��6    5]      �� 5^      $��1* �6    �� 5_      $��2* �6    �� 5`      $��1* �6    �� 5a      $��1* �6    .+�5c      �� $��0��6   $
��Left=�6%   $��	10,80,0,0q�6@   $��maxNumberOfTests��6S   $
��Wrap=�6k   $	��Top=�6   $	��120��6�   $��10(�6�   $��Italic$�6�   ���	#FF720BB76�   �� 5d      $��0��6   $��
10,100,0,0q�6%   $
��Wrap=�6[   $
��Left=�6o   $	��Top=�6�   $	��185��6�   $��20��6�   ��69   �� *maxNumberOfTests+6�   �� 5e      $��1��6   $
��Left=�6%   $��
10,0,10,10q�6@   $��Details��6T   $
��Wrap=�6c   $	��Top=�6w   $��88��6�   ���	#FF720BB7$��Italic$�6�   $��11(�6�   �� 5f      -   5        $��listBox_details��5f      $��1��6-   $��0,20,5,0q�6:   #��[ 5h      ��5g      �� #
 [  �� Path$ 	DataItems�5h      �� 5i      $��2��6   $��5,5,0,0q�6%   $
��Left=�66   $
��Wrap=�6Q   $	��Top=�6e   $	��107��6}   $	��210��6�   ��5j      ��  �� ElementName$ listBox_DataOnGrid��$ SelectedItem�# [ �� 5k      $��3��6   $��5,5,0,0q�6%   $
��Left=�66   $
��Wrap=�6Q   $	��Top=�6e   $	��107��6}   $	��210��6�   ��5l      �� $ listBox_DataOnGrid��$ SelectedItem�# [ 5m      �� 5n   
   -   5        $��commandsPanel��5n      $ 
Horizontal=�6*   $��
0,10,0,623q�6C   $��2��6W   .+�5o      �� $
��Open��6   $��2��5p      $
��Left=�6%   $��10,5,0,0q�6@   $	��Top=�6R   ��5o   $   �� *OpenCommand+5p   R   �� 5q      $
��Save��6   $��2��5r      $
��Left=�6%   $��10,5,0,0q�6@   $	��Top=�6R   ��5q   $   �� *SaveCommand+5r   R   �� 5s      $��
Add Custom��6   #��Z 6*   $��2��5t      $
��Left=�6%   $��10,5,0,0q�6@   $	��Top=�6R   �� 5u      $��Remove��6   $ ��ApplicationCommands.Delete��6&   $��2��5v      $
��Left=�6%   $��10,5,0,0q�6@   $	��Top=�6R   $��2��6j   5w      5x      5y      