   M S B A M L   `   `   `  ����A  =MyShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:MyShop.UserControlsMyShop.UserControls  5   	   U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5   	       MyShop.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   0uc!clr-namespace:MyShop.UserControls     5   	   c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5   	   @lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf   5   	     � Title$  
MainWindow��5   	   $	��760��6   $
��1280��6)    � 
ResizeMode$ NoResize=�66    � AllowsTransparency$
 True��6L    � WindowStyle$
 None=�6f    � WindowStartupLocation$ CenterScreen=�5   	   
������  � 	Resources 5	         	boolToVis��%   &	        �� 5
   
   6'   5      .��5      ��  �� CornerRadius$ 25��6    �� Padding$ 20q�6   ���#151f2d._�5   
   �  � ColumnDefinitions 5      �� 5      $	��100 �6#   �� 5      $��* �6#   .+�5      �� ���#1e2e44$ 	25 0 0 25��6*   ._�5      � .+�5      �� $	��Top=�6!   .+�5      �� 	 �� Orientation$	 
Horizontal=�6%   $��Center=�6>   $��	0 30 0 40q�6[   .+�5      A� $��8��6&   $��8��60   ���#eb17006;   A� 5      $��8��6&   $��8��60   ���#eb1700$	��7 0q�6J   A� 5      $��8��6&   $��8��60   ���#eb17006;   5      $   MyShop.UserControls.MenuButton  5      $   MyShop.UserControls.MenuButton
   MouseDoubleClick$
 dashboard_Click��6;      Icon$ HomeVariant=�6(   6;     5      $
 productManager_Click��67   $ Package=�6(   67     5      $
 orderManager_Click��64   $
 Cart=�6(   64     5       $
 setting_Click��64   $
 Cogs=�6(   64     5!      $
 analystBudget_Click��67   $ Finance=�6(   67   5#        5$      $ Power=�6$   $��Bottom=�61   $��	 0 0 0 15q�6L   5&      5'      �� 5(      -   5         ��Name$ contentControl��5(      $��1��65   5*      5+      5,      5-      