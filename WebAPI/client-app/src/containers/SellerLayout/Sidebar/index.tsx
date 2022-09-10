import {
    ListItem,
    ListItemText,
    ListItemIcon
} from '@mui/material';

import { FC, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
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
    const { t } = useTranslation()
    const [menuItems, setMenuItems] = useState<Array<IMenuItem>>([
        { lable: `${t('containers.admin_seller.sideBar.characteristicGroups')}`, path: '/seller/characteristicGroups', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.characteristicNames')}`, path: '/seller/characteristicNames', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.characteristicValues')}`, path: '/seller/characteristicValues', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.products')}`, path: '/seller/products', rotate: undefined },
        { lable: "orders", path: '/seller/orders', rotate: undefined }
    ]);
    const [selected, setSelected] = useState<string>("");
    const location = useLocation();

    useEffect(() => {
        let newArr = [...menuItems];
        newArr.forEach(element => {
            if (element.path == location.pathname) {
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