   M S B A M L   `   `   `  ����N  JG4-EmployeeRegister, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:G4_EmployeeRegister.ViewsG4_EmployeeRegister.Views  5      S.clr-namespace:G4_EmployeeRegister.UserControls G4_EmployeeRegister.UserControls  5      /    )G4_EmployeeRegister.Views.VerFichajesPage   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:G4_EmployeeRegister.Views     5      =uc.clr-namespace:G4_EmployeeRegister.UserControls     5        N� Title$  VerFichajesPage��5
       N� 
Background 5      �� 5   
    �� ImageSource$# ../Resources/Fondos/fondo.jpg��6    j� Stretch$
 Fill=�6A    �� Opacity$	 0.5Q�6P   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��10q�6   $��Center=�6(    � ColumnDefinitions 5      �� 5      $��4* �6#   �� 5      $��* �6#   .+�5       7   1G4_EmployeeRegister.UserControls.UsersControlador  $
��Left=�6"   $��Center=�6=   �� 5#      -   5         ��Name$ 
BotonSalir��5#      $��1��5$      $��Center=�5%      
������ $��0q�5'      ��5(      �� *VolverAtrasCommand+.��5)      �� $!��/Resources/Iconos/salir.png��6   $��38��5*         EstiloImagen#��[  5+      5,      5-      � 50   
   $��1��6   $��10q�6   .+�51      �� $��Center=�6   $	��350��66   $��59��6B   .+�52      ��   EstiloTextoTitulo#��[ 53      ��52      �� *NombreCompleto+53      54      55      �� 58   
   $��2��6   $��10q�6   
�������$��1q�6=    �� CornerRadius$ 5��6Q   ._�59      &   System.Windows.Controls.DataGrid  -   5        	  Name$	 HistorialFichaje��59      
   AutoGenerateColumns$
 False��5:      $��Stretch=�5;      $��Stretch=�5<         ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5=         
IsReadOnly$
 True��5>         CanUserAddRows$ False��5@         CanUserDeleteRows$ False��5A      $
��Auto=�5B      $��Disabled=�5C      ��5?      �� *
Fichajes+   Columns 5D      0  *System.Windows.Controls.DataGridTextColumn  5E      ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6)   1  +System.Windows.Controls.DataGridBoundColumn   Binding 65   �� *	IdUsuario+  5F      $ 	FechaHora��6)    6<   �� *	FechaHora+  5G      $
 Tipo��6)    67   �� *Tipo+  5H      $ Observaciones��6)    6@   �� *Observaciones+5J      5K      5L      5M      