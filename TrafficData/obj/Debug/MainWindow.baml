   M S B A M L   `   `   `  ����|  xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting  5   	    TrafficData   TrafficData.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	   $��630.018��5	   	   $��1210.621��5
   	     � Title$  Traffic Data Analyzer��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   G    � DataContext 6	   �� *Main+   Locator �� Source# [   � 	Resources 5       �� MergedDictionaries �� Source�� 5   
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
��Auto �6    .+�5.      �� $��10,0,0,0q�6   $��0��6&   $��1��66   $��75��6C   $��75��6N   $��DB/logo.png��6Z   6q   �� 50      $��15,0,0,0q�6   $��1��6)   $��1��69   $��Center=�51      $��Center=�52      ��53      �� *	TimeUnits+ �� SelectedValue 54      �� *SelectedTimeUnit+(  "System.Windows.Controls.DatePicker  56      $��15,0,0,0q�6   $��2��6,   $��1��6<   $��Center=�57      $��Center=�58         SelectedDate 59      �� *SelectedDate+�� 5;      $��15,0,0,0q�6   $��3��6+   $��1��6;   $��12(�5<      $
��Bold"�5=        	WhiteFont#��[ 5>       	 Robot#��[ 5?      $��Center=�5A      $��Center=�5B      ��5@      �� *CurrentDate+5B      �� 5D      $��50,0,0,0q�6   $��4��6(   $��1��68   $��	Dashboard��5E      {�.    ButtonPressed#��[ 6;   6d   �� 5G      -   5        $��15,0,0,0q�5G      $��5��6)   $��1��69   $��Search��5H        
Navigation#��[ 5I   *   6P   �� 5K      -   5        $��15,0,0,0q�5K      $��6��6)   $��1��69   $��About Us��5L      #��[ 5M   ,   6R   �� 5O      -   5        $��15,0,0,0q�5O      $��7��6)   $��1��69   $��Contact��5P      #��[ 5Q   ,   6R   5S      � 5T   
   -   5        $ 
TotalsGrid��5T      $��1��6#   ���#616161
 5U      �� 5V      $
��Auto �6#   �� 5W      $
��Auto �6#   �� 5X      $
��Auto �6#   �� 5Y      $
��Auto �6#   	 5\      �� 5]      $
��Auto �6    �� 5^      $
��Auto �6    �� 5_      $
��Auto �6    .+�5c      �� $��0��6   $��0��6(   $��18(�5d      $
��Bold"�5e      #��[ 5f      #��[ 5g      $��Total Vehicles��5h      $��Center=�5i      $��Center=�5j      �� 5m      $��0��6   $��1��6(   $��12(�5n      $
��Bold"�5o      #��[ 5p      #��[ 5q      $��Center=�5s      $��Center=�5t      ��5r      �� *TotalVehicles+5t      >   8System.Windows.Controls.DataVisualization.Charting.Chart  5w      -   5        $��totalVehiclesChart��5w   k   $��0��6$   $��2��64   $	��300��6A   $��center=�6N   $��Center=�6�   $	��300��6�   $��0q�6�   
�������   Series. 5x      E   ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  H   BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   DependentValuePath$ Value��6/      IndependentValuePath$	 Key��6J      ItemsSource 6e   �� *VehicleChartData+�   �System.Windows.Controls.DataVisualization.Charting.ColumnBarBaseSeries`1[[System.Windows.Controls.DataVisualization.Charting.ColumnDataPoint, System.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   IndependentAxis 5y      E   ?System.Windows.Controls.DataVisualization.Charting.CategoryAxis  5z      =   7System.Windows.Controls.DataVisualization.Charting.Axis   Orientation$ X=�67   D   >System.Windows.Controls.DataVisualization.Charting.DisplayAxis   AxisLabelStyle 5{      �� 5|   "    �� 
TargetTypeB	   <System.Windows.Controls.DataVisualization.Charting.AxisLabel" 	 6(   .�5}   &   ��  �� Property � �� �� Value 5~   *   �� 5   .    �� 
TargetType" 	 6>   .R�5�   2   ��  	  FormattedContent#��z 6<    � LayoutTransform 5�   6   �� 5�   :    �� Angle$	 -90Q�6J   5�   3   5�   /   5�   '   5�   #   5�      5�      5�      �� 5�      $��2��6   $��0��6(   $��18(�5�      $
��Bold"�5�      #��[ 5�      #��[ 5�      $��Heavy Vehicles Percentage��5�      $��Center=�5�      $��Center=�5�      �� 5�      $��2��6   $��1��6(   $��12(�5�      $
��Bold"�5�      #��[ 5�      #��[ 5�      $��Center=�5�      $��Center=�5�      ��5�      �� *TotalGrossWeight+ �� StringFormat$
 {0}%��5�        5�      -   5        $��heavyVeh��5�   N   $��2��5�   $   $��2��64   $	��300��5�   $   $��center=�61   $��Center=�6^   $	��300��6y   $��0q�6�   . 5�      B
   <System.Windows.Controls.DataVisualization.Charting.PieSeries
  $ Value��6,   $	 Key��6G    5�   ,   �� *GrossWeightChartData+5�      5�      �� 5�      $��1��6   $��0��6(   $��18(�5�      $
��Bold"�5�      #��[ 5�      #��[ 5�      $��Average Speed��5�      $��Center=�5�      $��Center=�5�      �� 5�      $��1��6   $��1��6(   $��12(�5�      $
��Bold"�5�      #��[ 5�      #��[ 5�      $��Center=�5�      $��Center=�5�      ��5�      �� *AverageSpeed+5�        5�      -	   5        $��averageSpeedChart��5�   N   $��1��5�   $   $��2��64   $	��300��5�   $   $��center=�61   $��Center=�6g   $	��300��6�   $��0q�6�   
�������. 5�      C   =System.Windows.Controls.DataVisualization.Charting.LineSeries  $ Value��6-   $	 Key��6H    6c   �� *AverageSpeedChartData+�   �System.Windows.Controls.DataVisualization.Charting.LineAreaBaseSeries`1[[System.Windows.Controls.DataVisualization.Charting.LineDataPoint, System.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]   IndependentAxis 5�        5�      $ X=�67    5�      �� 5�   "   " 	 6(   .�5�   &   ��  � �� 5�   *   �� 5�   .   " 	 6>   .R�5�   2   �� #��z 6<    5�   6   �� 5�   :   $	 -90Q�6J   5�   3   5�   /   5�   '   5�   #   5�      5�      5�      �� 5�      $��3��6   $��0��6(   $��18(�5�      $
��Bold"�5�      #��[ 5�      #��[ 5�      $��	Lane Used��5�      $��Center=�5�      $��Center=�5�      � 5�      -
   5        $ laneGrid��5�      $��3��6%   $��1��65   $��Center=�6B   $��Center=�6_   
 5�      �� 5�      $
��Auto �6'   	 5�      �� 5�      $
��Auto �6$   �� 5�      $
��Auto �6$   .+�5�      �� $��0��6   $��0��6,   $��12(�5�      $
��Bold"�5�      #��[ 5�      #��[ 5�      $��Center=�5�      $��Center=�5�      ��5�      ��  �� Path$ MostUsedLane�$  Most Frequently Used : {0}��5�      �� 5�      $��0��6   $��1��6,   $��12(�5�      $
��Bold"�5�      #��[ 5�      #��[ 5�      $��Center=�5�      $��Center=�5      ��5�      �� $ LeastUsedLane�$! Least Frequently Used : {0}��5      5       5     -   5        $��usedLaneChart��5  N   $��3��5  $   $��2��64   $	��300��5  $   $��center=�61   $��Center=�6c   $	��300��6~   $��0q�6�   . 5     
  $ Value��6,   $	 Key��6G    5  ,   �� *LaneUsedChartData+5     5
     5     5     5     