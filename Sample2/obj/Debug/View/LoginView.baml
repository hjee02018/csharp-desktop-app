   M S B A M L   `   `   `  ����B  >Sample2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Sample2.ViewSample2.View  5   	       Sample2.View.LoginView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Sample2.View     5   	     � Title$  	로그인��5   	   $	��350��6   $	��520��6"   .��5	      �  J� RenderTransformOrigin$ 0.492,0.251&�6    � RowDefinitions 5
   
   �� 5      $��80 �6   �� 5      $
��Auto �6   $��57.96��6*   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��10,10,369,19q�6   $��/Image2.png��63   $
��Fill=�6H   �� 5   
   $��1��6   $��20,20,20,20q�6"    �� Orientation$ 
Horizontal=�67   .+�5      �� $��사용자 ID:��6   $��18(�6'   $��Center=�65   �� 5      -   5         ��Name$ 	txtUserId��5      $��15,0,0,0q�6)   $��18(�6;   $	��224��6I   $��44��6U   5      �� 5   
   $��2��6   $��20,20,20,20q�6"   $ 
Horizontal=�67   .+�5      �� $��비밀번호:��6   $��Center=�6%   $��18(�6@   G� 5      -   5         G�Name$ txtPassword��5      $��19,0,0,0q�6/   $��18(�6A   $	��224��6O   $��42��6[   5      �� 5   
   -   5        $��4��5      $��Login��6   $��13(�6F   $��SemiBold"�6T   $
��Left=�6j   $��410,0,0,-34q�6�   $��70��6�   5      5      