   M S B A M L   `   `   `  ����
  MyShop"    MyShop.UserControls.InfoCard   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      c _MahApps.Metro.IconPacks.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137ag cMahApps.Metro.IconPacks.Material, Version=4.0.0.0, Culture=neutral, PublicKeyToken=0c0d510f9915137a:Icon-http://metro.mahapps.com/winfx/xaml/iconpacks   5      -   5        $��infoCard��5      .��5      �� $��0 0 0 20q�6   ��5   
   �� 5        �� 
TargetType"  ��6      
mainBorder �� BasedOn# [  6(   .�5	      ��  �� Property � �� �� Value
 ���� 60   �� 5
       � ��$	 1.5q�64    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value$
 True��65   . �5      ��  � �� �#7166f968   5      �� 5       �� Binding 6"   ��  �� ElementName$ infoCard��	 �� Path$	 IsActive�
 �� Value
 True6Z   .a�5      ��  � �� �#7166f968   5      5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#   �� 5      $
��auto �6#   .+�5      �� ���#f3f3f3$��26(�6`   $
��Bold"�6n   $��Center=�6�   $��	10 0 40 0q�6�   ��6   �� $ infoCard��$	 Number�6�   �� 5!      $��1��6   .+�5"      �� $��13(�6   ��5#      �� 5$      "  ��6    .�5%      ��  � �� �#7166f96;    5&      �� 5'   "    6.   �� $ infoCard��$	 IsTechnical�
 True6i   .a�5(   &   ��  � �� �#7166f96C   �� 5)   &    � ��$ Technical Hiring��6=   5*   #   �� 5+   "    6.   �� $ infoCard��$	 IsTechnical�	
 False6i   .a�5,   &   ��  � �� �#b231576C   �� 5-   &    � ��$ Technical Hiring��6=   5.   #   50      52      �� 53      ���#dfe1e3$��16(�6c   $��0 5 0 0q�6q   $��SemiBold"�6�   ��6   �� $ infoCard��$	 Title�6�   55      .  (MahApps.Metro.IconPacks.PackIconMaterial  56         Kind$ 
ArrowRight=�6$   $��2��66   ���#dfe1e3$��17��6[   $��17��6g   $��Center=�6r   $��0 0 10 0q�6�   57      58      59      