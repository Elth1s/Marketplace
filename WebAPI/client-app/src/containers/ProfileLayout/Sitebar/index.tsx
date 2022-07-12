import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemIcon from '@mui/material/ListItemIcon';

import PersonOutlinedIcon from '@mui/icons-material/PersonOutlined';
import RedeemOutlinedIcon from '@mui/icons-material/RedeemOutlined';
import ShoppingCartOutlinedIcon from '@mui/icons-material/ShoppingCartOutlined';
import FavoriteBorderOutlinedIcon from '@mui/icons-material/FavoriteBorderOutlined';
import ChatBubbleOutlineIcon from '@mui/icons-material/ChatBubbleOutline';

import { Link } from "react-router-dom";

const list = [
    { label: 'User Name', path: '/cabinet/information', icon: <PersonOutlinedIcon/>},
    { label: 'Coupons', path: '/cabinet/coupons', icon: <RedeemOutlinedIcon/>},
    { label: 'My Order', path: '/cabinet/order', icon: <ShoppingCartOutlinedIcon/>},
    { label: 'selected products', path: '/cabinet/product', icon: <FavoriteBorderOutlinedIcon/>},
    { label: 'my reviews', path: '/cabinet/reviews', icon: <ChatBubbleOutlineIcon/>},
]

const Sitebar = () => {
    return (
        <List>
            {list.map((item, index) => (
                <ListItem key={index} component={Link} to={item.path}>
                    <ListItemIcon>
                        {item.icon}
                    </ListItemIcon>
                    <ListItemText primary={item.label} />
                </ListItem>
            ))}
        </List>
    );
}

export default Sitebar;