   M S B A M L   `   `   `  ����E  ATitleSoftT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:TitleSoftT.ViewTitleSoftT.View  5   	   $    TitleSoftT.View.AddTitleAndPic   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:TitleSoftT.View     5   	     � Title$  AddTitleAndPic��5   	   $	��450��6    $	��800��6-   .��5	      � .+�5
   
   �� -   5        $��PicStackPanel��5
       �� Orientation$ 
Horizontal=�6*   $	��400��6C   .+�5      �� $��标题信息��6   $��10 20 10 10q�6%   .��5      �� $ Vertical=�6   $��0,10,0,0q�64    � 	Resources 5       �� 
TargetType �� Property �� Value �� BasedOn��%�  '��       '�� �     7��'�� �     7��'	� I    7	��� 5      " ��6    .�5      ��  �@��Orientation$ 
Horizontal=�6<   �� 5       � ��$ 0,0,0,4q�67   5      �� 5      " ��6    8   .�5      ��  � ��$	 100��66   �� 5       � ��$ Center=�6B   5      �� 5      " ��6    8   .�5      ��  � ��$	 0,3q�68   5      �� 5      " 	�6    8   .�5      ��  � ��$	 0,3q�68   5      5      .+�5      �� .+�5      �� $��原关键字��6     �� Target 6/   ��  �� ElementName$ UserName���� 5       $	��150��6"   ��5!   "   �� *
OldTitle+	 �� UpdateSourceTrigger$	 PropertyChanged=�
 �� ValidatesOnDataErrors
 . .��6|   5"      5#      �� 5$      .+�5%      �� $��现关键字��6     6/   �� $ 	UserEmail���� 5&      $	��150��6"   ��6.   �� *
NewTitle+$	 PropertyChanged=�
 . 5'      �� 5(      .+�5)      �� $��录入时间��6     6/   �� $ 	UserPhone���� 5*      $	��150��6"   ��6.   �� *ValidateUI.UserPhone+$	 PropertyChanged=�
 . 5+      �� 5,      .+�5-      �� $��用户名字��6     6/   �� $ 	UserPhone���� 5.      $	��150��6"   ��6.   �� *
UserName+$	 PropertyChanged=�
 . 5/      �� 50      .+�51      �� $��用户编号��6     6/   �� $ 	UserPhone���� 52      $	��150��6"   ��6.   �� *UserNo+$	 PropertyChanged=�
 . 53      �� 57      $��提  交��6   $��
100,16,0,0q�6,      
MyButStyle#���   6@   $
��Left=�6f   ��6�   �� *	SubmitCmd+58      59      �� 5:      -   5         ��Name$ listviewPic��5:      $	��380��6-   $��10q�6:   ��6F   �� *
PicItmes+ �� View 5;      �� 5<      P�5=      �� 5>      -   5        gvc2�� �� Width$	 400��5>   ;   ��5?   "   �� 5@   &   图片列Title6;    �� CellTemplate 5B   "   �� 5C   &   .R�5F   *   �� $	��400��60   ��6=   �� *Url+5G   '   5I      5X      5Z      5\      �� 5]   
   5_      5`      5a      