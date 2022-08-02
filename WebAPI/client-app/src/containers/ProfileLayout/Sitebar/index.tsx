import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemIcon from '@mui/material/ListItemIcon';

import { useEffect, useState } from 'react';

import LinkRouter from '../../../components/LinkRouter';
import {
    black_user, orange_user,
    black_eye, orange_eye,
    black_shopping_cart, orange_shopping_cart,
    black_heart, orange_heart,
    black_review, orange_review,
} from '../../../assets/icons';

import { ListItemStyle } from './styled';
import { useLocation } from 'react-router-dom';

export interface IMenuItem {
    label: string,
    path: string,
    icon: string,
    activeIcon: string,
}

const menuItems = [
    { label: 'User Name', path: '/profile/information', icon: black_user, activeIcon: orange_user },
    { label: 'Reviewed products', path: '/profile/reviewed-products', icon: black_eye, activeIcon: orange_eye },
    { label: 'My Order', path: '/profile/order', icon: black_shopping_cart, activeIcon: orange_shopping_cart },
    { label: 'Selected products', path: '/profile/selected-product', icon: black_heart, activeIcon: orange_heart },
    { label: 'My reviews', path: '/profile/reviews', icon: black_review, activeIcon: orange_review },
];

const Sitebar = () => {
    const [selectedItem, setSelectedItem] = useState<string>("");
    const location = useLocation();

    useEffect(() => {
        setSelectedItem(location.pathname);
    }, []);

    return (
        <List>
            {menuItems.map((item, index) => (
                <LinkRouter key={index} underline="none" color="unset" to={item.path}>
                    <ListItem>
                        <ListItemStyle selected={selectedItem === item.path} onClick={() => setSelectedItem(item.path)}>
                            <ListItemIcon>
                                <img
                                    style={{ width: "20px", height: "20px" }}
                                    src={selectedItem === item.path ? item.activeIcon : item.icon}
                                    alt="icon"
                                />
                            </ListItemIcon>
                            <ListItemText primary={item.label} />
                        </ListItemStyle>
                    </ListItem>
                </LinkRouter>
            ))}
        </List>
    );
}

export default Sitebar;