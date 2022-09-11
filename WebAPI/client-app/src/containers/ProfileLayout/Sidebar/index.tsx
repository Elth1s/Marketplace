import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemIcon from '@mui/material/ListItemIcon';

import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';

import LinkRouter from '../../../components/LinkRouter';
import {
    black_user, orange_user,
    black_eye, orange_eye,
    black_shopping_cart, orange_shopping_cart,
    black_heart, orange_heart,
    black_review, orange_review,
} from '../../../assets/icons';

import { ListItemButtonStyle } from './styled';
import { useTranslation } from 'react-i18next';
import { useTypedSelector } from '../../../hooks/useTypedSelector';

interface IMenuItem {
    label: string,
    path: string,
    icon: string,
    activeIcon: string,
}



const Sidebar = () => {
    const { t } = useTranslation();

    const { user } = useTypedSelector(state => state.auth);

    const [menuItems, setMenuItems] = useState<Array<IMenuItem>>([
        { label: `${user.firstName} ${user.secondName}`, path: '/profile/information', icon: black_user, activeIcon: orange_user },
        { label: `${t("pages.user.menu.reviewedProducts")}`, path: '/profile/reviewed-products', icon: black_eye, activeIcon: orange_eye },
        { label: `${t("pages.user.menu.myOrders")}`, path: '/profile/orders', icon: black_shopping_cart, activeIcon: orange_shopping_cart },
        { label: `${t("pages.user.menu.selectedProducts")}`, path: '/profile/selected-products', icon: black_heart, activeIcon: orange_heart },
        // { label: `${t("pages.user.menu.myReviews")}`, path: '/profile/reviews', icon: black_review, activeIcon: orange_review },
    ]);

    const location = useLocation();

    const [selectedItem, setSelectedItem] = useState<string>("");

    useEffect(() => {
        setSelectedItem(location.pathname);
    }, []);

    return (
        <List
            sx={{
                padding: "0",
                "&& .MuiTouchRipple-child": {
                    backgroundColor: "transparent"
                }
            }}
        >
            {menuItems.map((item, index) => (
                <LinkRouter key={index} underline="none" color="unset" to={item.path}
                    sx={{
                        display: "block",
                        marginBottom: "20px",
                        "&:last-child": {
                            marginBottom: "0px"
                        }
                    }}
                >
                    <ListItem sx={{ padding: "0" }}>
                        <ListItemButtonStyle selected={selectedItem === item.path} onClick={() => setSelectedItem(item.path)}>
                            <ListItemIcon
                                sx={{
                                    minWidth: "20px",
                                    marginRight: "20px"
                                }}
                            >
                                <img
                                    style={{ width: "20px", height: "20px" }}
                                    src={selectedItem === item.path ? item.activeIcon : item.icon}
                                    alt="icon"
                                />
                            </ListItemIcon>
                            <ListItemText primary={item.label} />
                        </ListItemButtonStyle>
                    </ListItem>
                </LinkRouter>
            ))}
        </List>
    );
}

export default Sidebar;