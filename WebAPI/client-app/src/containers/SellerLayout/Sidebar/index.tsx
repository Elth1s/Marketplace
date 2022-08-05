import {
    ListItem,
    ListItemText,
    ListItemIcon
} from '@mui/material';

import { FC, useEffect, useState } from 'react';
import { useLocation } from "react-router-dom";
import { BooleanSchema } from 'yup';
import LinkRouter from '../../../components/LinkRouter';

import { DrawerStyle, RotatedBox, ListItemButtonStyle } from './styled';

export interface IDrawer {
    open: boolean,
}

export interface IMenuItem {
    lable: string,
    path: string,
    rotate?: boolean
}

const Sidebar: FC<IDrawer> = ({ open }) => {
    const [menuItems, setMenuItems] = useState<Array<IMenuItem>>([
        { lable: 'Characteristic Group', path: '/seller/characteristicGroup', rotate: undefined },
        { lable: 'Characteristic Name', path: '/seller/characteristicName', rotate: undefined },
        { lable: 'Characteristic Value', path: '/seller/characteristicValue', rotate: undefined },
        { lable: 'Product ', path: '/seller/product', rotate: undefined },
    ]);
    const [selected, setSelected] = useState<string>("");
    const location = useLocation();

    useEffect(() => {
        const pathnames = location.pathname.split('/').filter((x) => x);
        let newArr = [...menuItems];
        newArr.forEach(element => {
            if (element.path.split('/').filter((x) => x)[1] == pathnames[1]) {
                element.rotate = true;
                setSelected(element.lable);
            }
        });
        setMenuItems(newArr);
    }, []);

    const changeSelected = (id: number) => {
        let newArr = [...menuItems];
        newArr.forEach(element => {
            if (element.rotate == true)
                element.rotate = false;
        });
        newArr[id].rotate = true;
        setSelected("");
        setMenuItems(newArr);
    }

    return (
        <DrawerStyle variant="permanent" open={open}>
            {menuItems.map((item, index) => (
                <LinkRouter key={index} underline="none" color="unset" to={item.path}>
                    <ListItem disablePadding>
                        <ListItemButtonStyle onClick={() => changeSelected(index)}>
                            <ListItemIcon sx={{ minWidth: "auto" }}>
                                <RotatedBox rotate={item.rotate} isRotated={selected === item.lable ? true : false} />
                            </ListItemIcon>
                            <ListItemText primary={item.lable} />
                        </ListItemButtonStyle>
                    </ListItem>
                </LinkRouter>
            ))}
        </DrawerStyle >
    );
};

export default Sidebar;