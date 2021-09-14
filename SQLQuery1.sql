SELECT m.*, p.*, tp.* FROM Menu AS m
                        LEFT JOIN Composer c ON c.Id_menu= m.Id_menu
                        LEFT JOIN Plat p ON p.Id_plat=c.Id_plat
                        LEFT JOIN TypePlat tp ON p.Id_type=tp.Id_type
                        WHERE m.date = '18/08/2021' AND m.IsMidi= 1