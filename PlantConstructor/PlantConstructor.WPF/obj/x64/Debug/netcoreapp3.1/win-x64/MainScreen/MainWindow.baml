   M S B A M L   `   `   `  ����O  KPlantConstructor.WPF, Version=3.0.0.0, Culture=neutral, PublicKeyToken=nullQ-clr-namespace:PlantConstructor.WPF.MainScreenPlantConstructor.WPF.MainScreen  5   	   I)clr-namespace:PlantConstructor.WPF.HelperPlantConstructor.WPF.Helper  5   	   0    *PlantConstructor.WPF.MainScreen.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35h dDevExpress.Xpf.TypedStyles.v20.1, Version=20.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35a ]DevExpress.Xpf.Core.v20.1, Version=20.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aW SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089L 9http://schemas.microsoft.com/winfx/2006/xaml/presentation        5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089<x,http://schemas.microsoft.com/winfx/2006/xaml      5   	   Bd2http://schemas.microsoft.com/expression/blend/2008      5   	   l	 hDevExpress.Xpf.Core.v20.1.Extensions, Version=20.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a?dx2http://schemas.devexpress.com/winfx/2008/xaml/core  	  5   	   f
 bDevExpress.Xpf.Accordion.v20.1, Version=20.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aCdxa7http://schemas.devexpress.com/winfx/2008/xaml/accordion  
 5   	   Adxe5http://schemas.devexpress.com/winfx/2008/xaml/editors   5   	   j fDevExpress.Xpf.LayoutControl.v20.1, Version=20.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aHdxlc;http://schemas.devexpress.com/winfx/2008/xaml/layoutcontrol   5	   	   c _DevExpress.Xpf.Ribbon.v20.1, Version=20.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aBdxr4http://schemas.devexpress.com/winfx/2008/xaml/ribbon    5
   	   >dxb2http://schemas.devexpress.com/winfx/2008/xaml/bars   5   	   Fmc;http://schemas.openxmlformats.org/markup-compatibility/2006   5   	   e aDevExpress.Xpf.Controls.v20.1, Version=20.1.7.0, Culture=neutral, PublicKeyToken=b88d1754d700e49aFdxnav8http://schemas.devexpress.com/winfx/2008/xaml/navigation   5   	   ;local-clr-namespace:PlantConstructor.WPF.MainScreen   5   	   =localHelper)clr-namespace:PlantConstructor.WPF.Helper   5   	   -   5        &   DevExpress.Xpf.Core.ThemedWindow   Name$
  root��5   	    � Title$ Plant Constructor��5   	   $	��450��6#   $	��600��60    � 
ResizeMode$ CanMinimize=�5   	    � DataContext 5      9   3PlantConstructor.WPF.MainScreen.MainWindowViewModel  5   
    � 	Resources 5      <   6PlantConstructor.WPF.Helper.EditProjectValuesConverter    myEditProjectValuesConverter  projectDetailsTemplate.  (DevExpress.Xpf.LayoutControl.LayoutGroup   Orientation(  "DevExpress.Xpf.Bars.ToolBarControl,  &DevExpress.Xpf.Bars.ToolBarControlBase   Caption   AllowCustomizationMenu   BarItemHorizontalIndent	   BarItemDisplayMode
   Items'  !DevExpress.Xpf.Bars.BarButtonItem!  DevExpress.Xpf.Bars.BarItem   Content)	  #DevExpress.Xpf.Core.ObjectConverter   Glyph*
  $DevExpress.Xpf.Core.DXImageExtension   Command �� ElementName   CommandParameter `� 	Converter �� Path.  (DevExpress.Xpf.Bars.BarItemLinkSeparator-  'DevExpress.Xpf.LayoutControl.LayoutItem   Label   AddColonToLabel   Content%  DevExpress.Xpf.Editors.TextEdit%  DevExpress.Xpf.Editors.BaseEdit   	EditValue �� Mode �� UpdateSourceTrigger �� SelectedItem � ColumnDefinitions � RowDefinitions �� SelectionMode �� 
TargetType �� Property �� Value  emptyDetailsTemplate J� Opacity  �� TextAlignment@   :PlantConstructor.WPF.Helper.ProjectDetailsTemplateSelector " projectDetailsTemplateSelector!   ProjectDetailsTemplate"   EmptyDetailsTemplate��%F  &	        &	      7   &	 
    &	 �    7  7    5   
   61   �� 5   
   .R�5        $ Vertical=�6   .+�5        $	��Top=�6#   .+�5        $ Main Toolbar��6)   $ False��6@   $ 10Q�6_   $	 ContentAndGlyph=�6|   .
 5        $ Save Changes	 6,    6C   
  *Images/Save/Save_16x16.png+ 6u   �� *&$DataContext.SaveProjectButtonCommand+$
 root�� 5      `� 5   "   8   .1�5   &   �� $ 	EditValue�6.   $ txtEditProjectName��6?   �� 5    &   $ 	EditValue�6.   $ txtEditProjectGroup��6?   5!   #   5#        5$        5%      $ 	Edit Data	 6,    6@   
  *Images/Edit/Edit_16x16.png+ 6r   �� *#!DataContext.EditDataButtonCommand+$
 root��  5&        5'      $ Delete Project	 6,    6E   
  *Images/Edit/Delete_16x16.png+ 6y   �� *(&DataContext.DeleteProjectButtonCommand+$
 root��5(      5)        5*      $��Center=�6#   $ Vertical=�6>   .+�5+        $ Project Name��6&   $
 True��6;   . 5,        $��txtEditProjectName��6'   $	��200��6B    6N   �� *Name+$ OneWay=�$ PropertyChanged=�5-        5.      $ Project Group��6&   $
 True��6<   . 5/        $��txtEditProjectGroup��6'   $	��200��6B    6N   �� *ProjectGroup+$ OneWay=�$ PropertyChanged=�50        51      . 52      �� $	��100��6#   ��6/   �� *-+DataContext.ProjectAttributeGroupesComboBox+$
 root�� 53   #   �� *$"DataContext.SelectedAttributeGroup+$ TwoWay=�$ PropertyChanged=�$
 root��54        55      . 56      �  57      �� 58   "   $��* �63   �� 59   "   $��* �63    5;      �� 5<   "   $��30 �60   �� 5=   "   $	��200 �60   �� 5>   "   $��30 �60   �� 5?   "   $��30 �60   .+�5A      � $��0��6#   $��1��63   .+�5B   "     $��Stretch=�62   . 5C   &   �� $��Visible=�5D   .   $ Extended=�5F   .   ��5C   .   �� *.,DataContext.AllAvailableAttributesForDisplay+$ TwoWay=�$ PropertyChanged=�$
 root�� 5E   .   �� *,*DataContext.SelectedAttributeFromAvailable+$ TwoWay=�$ PropertyChanged=�$
 root����5G   *   �� 5H   .   .R�5I   2   �� .+�5J   6   �� ��6@   �� *Item+5K   3   5L   /   ��5N   *   �� 5O   .   " ��64   .�5P   2   ��  �@��
IsSelected 6O   �� $ TwoWay=�$ 
IsSelected�5Q   /   5S   '   5T   #   5U      � 5V      $��0��6#   $��2��63   .+�5W   "     $��Bottom=�62   . 5X   &   �� $��30��6-   
�������$��
Select All��6P   ��5Y   -   �� *75DataContext.SelectAllAvailableAttributesButtonCommand+$
 root��5Z   #   5[      � 5\      $��0��6#   $��3��63   .+�5]   "     $��Bottom=�62   . 5^   &   �� $��30��6-   
�������$	��>>>��6P   ��5_   -   �� *1/DataContext.AllAvailableAttributesButtonCommand+$
 root��5`   #   5a      � 5b      $��0��6#   $��0��63   .+�5c        $	��Top=�6.   . 5d   &   �� $��30��6,   $��Center=�68   $��Center=�6S   $��Available Attributes��6p   5e   #   5f      � 5g      $��1��6#   $��1��63   .+�5h   "     $��Stretch=�62   . 5i   &   �� $��Visible=�5j   .   $ Extended=�5l   .   ��5i   .   �� *,*DataContext.AllProjectAttributesForDisplay+$ TwoWay=�$ PropertyChanged=�$
 root�� 5k   .   �� **(DataContext.SelectedAttributeFromProject+$ TwoWay=�$ PropertyChanged=�$
 root����5m   *   �� 5n   .   .R�5o   2   �� .+�5p   6   �� ��6@   �� 5q   3   5r   /   ��5t   *   �� 5u   .   " ��64   .�5v   2   ��  �@��
IsSelected 6O   �� $ TwoWay=�$ 
IsSelected�5w   /   5y   '   5z   #   5{      � 5|      $��1��6#   $��2��63   .+�5}   "     $��Bottom=�62   . 5~   &   �� $��30��6-   
�������$��
Select All��6P   ��5   .   �� *53DataContext.SelectAllProjectAttributesButtonCommand+$
 root��5�   #   5�      � 5�      $��1��6#   $��3��63   .+�5�   "     $��Bottom=�62   . 5�   &   �� $��30��6-   
�������$	��<<<��6P   ��5�   -   �� */-DataContext.AllProjectAttributesButtonCommand+$
 root��5�   #   5�      � 5�      $��1��6#   $��0��63   .+�5�        $	��Top=�6.   . 5�   &   �� $��30��6,   $��Center=�68   $��Center=�6S   $��Project Attributes��6p   5�   #   5�      5�      5�      5�      5�      5�      �� 5�   
   .R�5�      � $��Stretch=�6   .+�5�      �� $&�� Select a project to see details.��6   $��16(�5�      $
 0.62Q�5�      $��Center=�5�      $  Center=�5�      $
��Wrap=�5�      5�      5�        5�   
   8!   8"  5�   ,   5�      .��5�      �  5�   
   �� 5�      $��2* �6   �� 5�      $��5* �6   .+�5�   
   � $��0��6   .+�5�        $ Vertical=�6   .+�5�        $��Stretch=�6"   . 5�      / 
 )DevExpress.Xpf.Accordion.AccordionControl  -   5        #  Name$# 	accordion��5�   -   $   
ExpandMode$$ SingleOrNone=�5�   !   %   SelectionUnit$% SubItem=�5�   !   &   ChildrenPath$& Projects��5�   !   '   ShowSearchControl$
' True��5�   !   $��Disabled=�5�   !   $��Stretch=�5�   !   
�������(   SelectedItem( 5�   !   �� *SelectedItem+)   ItemsSource) 5�   !   �� *ProjectDepartments+5�      5�        5�      $��Bottom=�6"   . 5�      �� 
�������
����  �$��+ Add Project��6H   $��30��6�   ��6`   �� *AddProjectButtonCommand+6�   5�        5�      $��Bottom=�6"   . 5�      �� 
�������
����  �$��Settings��6H   $��30��6�   ��6[   �� *OpenSettingsWindowCommand+6�   5�      5�      5�      � 5�   
   $��1��6   $��
10,10,10,0q�6   .+�5�        .+�5�        $��Stretch=�6"   . 5�      �� #��[ 5�      ��5�      �� *SelectedItem+5�      5�      5�      5�      5�      5�      