import {
    ListItem,
    Typography,
    ListItemIcon,
    ListItemText,
    useTheme
} from '@mui/material';

import { FC, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useLocation } from "react-router-dom";
import { BooleanSchema } from 'yup';
import LinkRouter from '../../../components/LinkRouter';

import { DrawerStyle, RotatedBox, ListItemButtonStyle } from './styled';

interface IDrawer {
    open: boolean,
}

interface IMenuItem {
    lable: string,
    path: string,
    rotate?: boolean
}

const Sidebar: FC<IDrawer> = ({ open }) => {
    const { t } = useTranslation()
    const { palette } = useTheme();

    const [menuItems, setMenuItems] = useState<Array<IMenuItem>>([
        { lable: `${t('containers.admin_seller.sideBar.sales')}`, path: '/admin/sales', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.categories')}`, path: '/admin/categories', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.characteristicGroups')}`, path: '/admin/characteristicGroups', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.characteristicNames')}`, path: '/admin/characteristicNames', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.characteristicValues')}`, path: '/admin/characteristicValues', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.filterGroups')}`, path: '/admin/filterGroups', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.filterNames')}`, path: '/admin/filterNames', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.filterValues')}`, path: '/admin/filterValues', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.products')}`, path: '/admin/products', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.productStatuses')}`, path: '/admin/productStatuses', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.shops')}`, path: '/admin/shops', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.countries')}`, path: '/admin/countries', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.cities')}`, path: '/admin/cities', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.deliveryTypes')}`, path: '/admin/deliveryTypes', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.users')}`, path: '/admin/users', rotate: undefined },
        { lable: `${t('containers.admin_seller.sideBar.orderStatuses')}`, path: '/admin/orderStatuses', rotate: undefined },
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
                            <ListItemText
                                sx={{
                                    "& .MuiListItemText-primary": {
                                        color: palette.mode == "dark" ? palette.common.white : palette.common.black
                                    }
                                }} primary={item.lable}
                            />
                        </ListItemButtonStyle>
                    </ListItem>
                </LinkRouter>
            ))}
        </DrawerStyle >
    );
};

export default Sidebar;