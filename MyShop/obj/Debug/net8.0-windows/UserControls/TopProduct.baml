   M S B A M L   `   `   `  ����
  MyShop$    MyShop.UserControls.TopProduct   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      -   5          >�Name$  
topProduct��5      .��5      ��    ListViewItemStyle1#���   6   $��0 1 0 0q�6@   ���#2c3a54$
��0 10q�6p   .��5   
   �  � ColumnDefinitions 5      �� 5	      $
��auto �6#   �� 5
      $	��170 �6#   �� 5      $��70 �6#   �� 5      $
��auto �6#   .+�5      �� $��18��6I   $��10��6T   $
��Fill=�6`   $��2 0 0 0q�6o   ��6   ��  �� ElementName$ 
topProduct�� �� Path$
 Flag�6o   �� 5      $��1��6R   ���#efefef$��Center=�6w   $��20 0 0 0q�6�   ��6   �� $ 
topProduct��$ ProductName�6�   �� 5      $��2��6L   ���#b1b6db$��Center=�6q   $��10 0 0 0q�6�   ��6   �� $ 
topProduct��$ Price�6�   .  (MahApps.Metro.IconPacks.PackIconMaterial  5      $��15��6$   $��15��60   $��4 0 0 0q�6;   $��3��6L   ��5      �� 5       �� 
TargetType"  6   .�5      ��  �� Property �@ Kind �� Value$ ChevronUpCircle=�61   �� 5       � �� �#19dc2d67    �� Triggers 5      �� 5       �� Binding 6*   �� $ country��$ 	IsLevelUp�	 �� Value	 True6b   .a�5   "   ��  �@ Kind$ ArrowRightTop=�69   �� 5   "    � �� �#19dc2d6?   5      �� 5       6*   �� $ country��$ 	IsLevelUp�		 False6b   .a�5    "   ��  �@ Kind$ ArrowRightBottom=�69   �� 5!   "    � �� �#f723536?   5#      5%      5'        5(      
   Kind$
 DotsVertical=�6$   ���#efefef$��15��6M   $��15��6Y   $��4��6d   $��35 0 3 0q�6t   5)      5*      5+      