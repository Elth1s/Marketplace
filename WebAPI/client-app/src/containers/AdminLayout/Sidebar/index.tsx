import {
    List,
    ListItem,
    ListItemButton,
    ListItemText,
    ListItemIcon
} from '@mui/material';

import { FC, useEffect, useState } from 'react';
import { useLocation, Link } from "react-router-dom";
import { BooleanSchema } from 'yup';

import { DrawerStyle, RotatedBox, ListItemButtonStyle } from './styled';

export interface IDrawer {
    open: boolean,
}

export interface IMenuItem {
    lable: string,
    path: string,
    rotete?: boolean
}

const Sidebar: FC<IDrawer> = ({ open }) => {
    const [menuItems, setMenuItems] = useState<Array<IMenuItem>>([
        { lable: 'Category', path: '/admin/category', rotete: undefined },
        { lable: 'Characteristic Group', path: '/admin/characteristicGroup', rotete: undefined },
        { lable: 'Characteristic', path: '/admin/characteristic', rotete: undefined },
        { lable: 'Country', path: '/admin/country', rotete: undefined },
        { lable: 'City', path: '/admin/city', rotete: undefined },
    ]);
    const [selected, setSelected] = useState<string>("");
    const location = useLocation();

    useEffect(() => {
        const pathnames = location.pathname.split('/').filter((x) => x);
        let newArr = [...menuItems];
        newArr.forEach(element => {
            if (element.path.split('/').filter((x) => x)[1] == pathnames[1]) {
                element.rotete = true;
                console.log(element.lable)
                setSelected(element.lable);
            }
        });
        setMenuItems(newArr);
    }, []);

    const changeSelected = (id: number) => {
        let newArr = [...menuItems];
        newArr.forEach(element => {
            if (element.rotete == true)
                element.rotete = false;
        });
        newArr[id].rotete = true;
        setSelected("");
        setMenuItems(newArr);
    }

    return (
        <DrawerStyle variant="permanent" open={open} >
            {menuItems.map((item, index) => (
                <ListItem key={index} component={Link} to={item.path} disablePadding style={{ textDecoration: 'none', color: 'unset' }} >
                    <ListItemButtonStyle onClick={() => changeSelected(index)}>
                        <ListItemIcon sx={{ minWidth: "auto" }}>
                            <RotatedBox rotete={item.rotete} isRoteted={selected === item.lable ? true : false} />
                        </ListItemIcon>
                        <ListItemText primary={item.lable} />
                    </ListItemButtonStyle>
                </ListItem>
            ))}
        </DrawerStyle >
    );
};

export default Sidebar;