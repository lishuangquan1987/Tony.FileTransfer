   M S B A M L   `   `   `  ����U  QTony.FileTransfer.Resource, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null}Uclr-namespace:Tony.FileTransfer.Resource.Controls;assembly=Tony.FileTransfer.Resource#Tony.FileTransfer.Resource.Controls  5   	   S OTony.FileTransfer.Client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:Tony.FileTransfer.ClientTony.FileTransfer.Client 5   	   )   #Tony.FileTransfer.Client.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   jresourceUclr-namespace:Tony.FileTransfer.Resource.Controls;assembly=Tony.FileTransfer.Resource     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   8local&clr-namespace:Tony.FileTransfer.Client     5   	     � Title$  
MainWindow��5	   	   $	��450��6    � Icon$ transfer.ico��6)   $	��800��6=   .��5
      � .+�5   
   �� $��30��6   $	��100��6   .��6)   test6/   � 5   
   $��3��6   $��30��6   $��90��6+   $��Right=�66   $	��Top=�6R    � 	Resources 5         miniMaxButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers	 P� Property
 P� Value  closeButtonStyle �Name �� 
TargetName��%  &	        &	     �� 5      " ��63   .�5      ��  � ��
 ���� 6I   �� 5       � ��$ 0q�68   6D   �� 5       � ��
 ���� 6J   �� 5       � ��$ 0q�60   6<   �� 5       � �� 5      �� 5      " ��6.   .R�5   "   �� $ bd��5   A   #��z��5   )   #��z��5   A   #��z��5   A   ._�5   &   �� $��Center=�68   $��Center=�6U   6r   5   #    5   "   P� 5   &   	 �@J�IsMouseOver
 . . �5    *   ��  � ��
 �����6V   5!   '   5#      5%      5&      �� 5'      " ��61   .�5(      ��  � ��
 ���� 6I   �� 5)       � ��$ 0q�68   6D   �� 5*       � ��
 ���� 6J   �� 5+       � ��$ 0q�60   6<   �� 5,       � �� 5-      �� 5.      " ��6.   .R�5/   "   � $
 grid��6@   
������ .+�50   &   �� $��Center=�67   $��Center=�6T   6q   51   #    52   "   P� 53   &   	 �@J�IsMouseOver
 . . �54   *   �� $
 grid��6S    � ��
 �  ��6g   55   '   57      59      5:      5;       � ColumnDefinitions 5<      �� 5=      6%   �� 5>      6%   �� 5?      6%   .+�5A      �� -   5         ��Name$ PART_MinimalButton��5A      $��30��61   $��30��6=   ��5B      �� 5C      " ��6   .�5D      ��  � ��
 ���� 6M   �� 5E       � ��$ 0q�6<   6H   �� 5F       � ��
 ���� 6N   �� 5G       � �� 5H      �� 5I   "   " ��62   .R�5J   &   �� $ bd��5L   A   #��z��5J   -   #��z��5K   A   ._�5M   *   �� $��Center=�6<   $��Center=�6Y   6v   5N   '    5P   &   P� 5Q   *   	 �@J�IsMouseOver
 . . �5R   .   �� $ bd��6X    � ��
 �����6j   5S   +   5U   #   5W      5X      .��5Z      �� 	 �� X1$ 7��6   	 �� X2$
 22.5��6   	 �� Y1$ 15��6(   	 �� Y2$ 15��60   $��1��69     Basic.Foreground#���  6M   6{   5[      �� 5\      -   5        $ PART_MaxMalButton��5\      $��1��60   #���   6@   5^      �� 5_      -   5        $ PART_CloseButton��5_      $��2��6/   $	��关��6?   5d      5e      5f      5g      