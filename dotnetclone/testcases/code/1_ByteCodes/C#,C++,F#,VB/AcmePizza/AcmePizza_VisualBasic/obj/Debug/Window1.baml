   M S B A M L   `   `   `  ����D  @AcmePizza, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:AcmePizza	AcmePizza  5          AcmePizza.Window1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      )localclr-namespace:AcmePizza     5      -   5          � Title$%  Acme Pizza Co. - Current Orders��5      $	��428��6-   $
��1038��6:    � WindowState$ 	Maximized=�5       � WindowStartupLocation$ CenterScreen=�64    � 
ResizeMode$ 	CanResize=�6Y   .��5      �  � RowDefinitions 5	   
   �� 5
      $��55 �6   �� 5      $
��243* �6    � ColumnDefinitions 5   
   �� 5      $	��539 �6   �� 5      $
��477* �6   .+�5   
   �� -   5         ��Name$ 
ordersList��5         PizzaOrderTemplate#��[  6C   $��1��5      $��2��6$   $	��Top=�68   $��0,0,0,0q�6P   ��5   +   �� ��5      �� 5      .R�5      � 5      ��5      �� 5      .�5      ��  �� Property � �� �� Value$	 205��66   �� 5       � ��$	 205��67   �� 5       � ��$	 2.5q�67   5      5       �� 5!   
   -   5        $��
textBlock1��5!       $��59��6   $	��Top=�62   $��Acme Pizza Co.��6J   
���P��$��38(�6s   $��
10,0,202,0q�6�   �� 5"   
   -   5        $��
textBlock2��5"   P   $��38(�6   
������	 �� TextAlignment$	 Right=�6:   $��Current Orders��6b   $��	198,0,0,0q�6x   $��1��6�   �� 5#   
   -   5        $��image1��5$      $��55��5#      $��	0,4,201,0q�6   $��UniformToFill=�5$   "   $	��Top=�6:   $��pizzaicon.png��6R   $��Right=�6i   $��62��6�   �� 5%   
   -   5        $��processNextOrderButton��5%      $��	0,13,0,12q�6T   $��Right=�6g   $	��164��6�   .��6�   Process Next Order6�   5&      5'      