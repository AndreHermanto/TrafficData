   M S B A M L   `   `   `  ����|  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5   	    TrafficData   TrafficData.ContactWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	   $��630.018��5	   	   $��1210.621��5
   	     � Title$  Traffic Data Analyzer��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   J    � DataContext 6	   �� *	AboutUs+   Locator �� Source# [   � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Skins/MainSkin.xaml?�6%   %    5      .��5      � -   5         �Name$ 
LayoutRoot��5      ���#212121	 � RowDefinitions	 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   � -   5        $ 
HeaderGrid��5      $��0��6#   $	��100��60   
 � ColumnDefinitions
 5      �� 5       $
��Auto �6#   �� 5!      $
��Auto �6#   �� 5"      $
��Auto �6#   �� 5#      $
��Auto �6#   �� 5$      $
��Auto �6#   �� 5%      $
��Auto �6#   �� 5&      $
��Auto �6#   �� 5'      $
��Auto �6#   	 5)      �� 5*      $��10 �6    �� 5+      $
��Auto �6    .+�5.      �� $��10,0,0,0q�6   $��0��6&   $��1��66   $��75��6C   $��75��6N   $��DB/logo.png��6Z   6q   �� 50      $��15,0,0,0q�6   $��1��6+   $��1��6;   $��18(�51      $
��Bold"�52        	WhiteFont#��[ 53       	 Robot#��[ 54      $��Contact��55      $��Center=�56      $��Center=�57      �� 59      $��90,0,0,0q�6   $��3��6+   $��1��6;   $��12(�5:      $
��Bold"�5;      #��[ 5<      #��[ 5=      $��Center=�5?      $��Center=�5@      ��5>      �� *CurrentDate+5@      �� 5B      -   5        $��50,0,0,0q�5B      $��4��6(   $��1��68     
Navigation#��[ 6F   $��	Dashboard��5C      5D   /   �� 5F      -   5        $��15,0,0,0q�5F      $��5��6)   $��1��69   #��[ 6F   $��Search��5G      5H   ,   �� 5J      -   5        $��15,0,0,0q�5J      $��6��6)   $��1��69   #��[ 6F   $��About Us��5K      6B   �� 5M      $��15,0,0,0q�6   $��7��6)   $��1��69   $��Contact��5N      {�.    ButtonPressed#��[ 69   5O      5Q      � 5R   
   -   5        $ AboutUsGrid��5R      $��1��6$   ���#616161$	��400��6F   
 5S      �� 5T      $
��Auto �6#   �� 5U      $
��Auto �6#   	 5X      �� 5Y      $��1 �6    �� 5Z      $
��Auto �6    �� 5[      $
��Auto �6    �� 5\      $
��Auto �6    �� 5]      $
��Auto �6    .+�5`      �� $��1��6   $��52,45,193,9q�6%   $��Director��6:   $��26(�6J   $��Center=�6X   $
��Wrap=�6u   $��Center=�6�    J� RenderTransformOrigin$ -0.606,0.593&�6�   #��[ 6�   #��[ 6�   5a      � 5c      $��2��6   $��50,0,0,0q�6    
 5d      �� 5e      $
��Auto �6'   �� 5f      $
��Auto �6'   	 5i      �� 5j      $
��Auto �6$   .+�5m      �� $��0��6   $��0��6(   $��80��65   $��DB/person.PNG��6@   $��0,0,1,19q�6W   �� 5n      $��1��6   $��0��6,   $
��Auto��69   #��[ 6F   #��[ 6n   $��10,0,-103,0q�6�   $��20(�6�   Paul Higgins�� 5o   #   paul.higgins@exceltech.com.au�� 5p   4   (123) 12390132�� 5q   %   5r      5s      �� 5u      $��3��6   $��50,42,48,10q�6%   $��Production Manager��6:   $��26(�6T   $��Center=�6b   $
��Wrap=�6   $��Center=�6�   #��[ 6�   #��[ 6�   5v      � 5x      $��4��6   $��50,0,0,0q�6    
 5y      �� 5z      $
��Auto �6'   �� 5{      $
��Auto �6'   	 5~      �� 5      $
��Auto �6$   .+�5�      �� $��0��6   $��0��6(   $��80��65   $��DB/personf.jpg��6@   $��0,-6,3,6q�6X   6l   �� 5�      $��1��6   $��0��6,   $
��Auto��69   #��[ 6F   #��[ 6n   $��
10,0,-77,0q�6�   $��20(�6�   
Susan Reid�� 5�       susanr@exceltech.com.au�� 5�   .   (123) 1321221�� 5�   $   5�      5�      5�      5�      5�      