   M S B A M L   `   `   `  ����N  JG4-EmployeeRegister, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:G4_EmployeeRegister.ViewsG4_EmployeeRegister.Views  5   	   S.clr-namespace:G4_EmployeeRegister.UserControls G4_EmployeeRegister.UserControls  5   	   )    #G4_EmployeeRegister.Views.LoginView   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:G4_EmployeeRegister.Views     5   	   =uc.clr-namespace:G4_EmployeeRegister.UserControls     5   	     � Title$  	LoginView��5	   	   $	��450��6   $	��800��6(    � 
ResizeMode$ NoResize=�5
   	   ��5      �� 5   
    �� ImageSource$# ../Resources/Fondos/fondo.jpg��6    j� Stretch$
 Fill=�6A    �� Opacity$	 0.5Q�6P   .��5      � $��Center=�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   7   1G4_EmployeeRegister.UserControls.UsersControlador  $��0��6   $��Center=�6+   �� 5   
   $��1��6      EstilosBordesFormulario#��[  5      $��Center=�5      $��Center=�5      $	��400��5      $	��300��5       $��0,20,0,0q�5!      ._�5"      �  5#      �� 5$      $��* �6$   �� 5%      $
��Auto �6$   �� 5&      $
��Auto �6$   �� 5'      $��* �6$   �� 5(      $
��Auto �6$   �� 5)      $
��Auto �6$   �� 5*      $��* �6$   �� 5+      $
��Auto �6$   �� 5,      $��* �6$    � ColumnDefinitions 5/      �� 50      $��* �6'   .+�54      �� $��1��6   $��Username��6)     EstilosSubtitulos#��[ 69   �� 55      -   5         ��Name$ UsuarioTextBox��55      $��2��62     EstilosTextBox#��[ 56      ��55   ?   �� *
Username+56      �� 59      $��4��6   $��Contraseña��6)   #��[ 5:      G� 5;      -   5         G�Name$ ContraseñaPasswordBox��5;      $��5��6>     EstilosPasswordBox#��[ 5<      6J   �� 5?      $��7��6   	 �� Orientation$	 
Horizontal=�6*   $��Center=�6C   $
��Auto��6`   .+�5A      �� $��Entrar��6     EstilosButton#��[ 6.   $	��140��6U   ��6a   �� *LoginCommand+5B      5C      5D      5E      5F      