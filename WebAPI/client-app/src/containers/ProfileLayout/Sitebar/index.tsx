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
import { black_user, black_eye, black_shopping_cart, black_heart, black_review } from '../../../assets/icons';

const list = [
    {
        label: 'User Name', path: '/cabinet/information', icon:
            <img
                style={{ width: "20px", height: "20px" }}
                src={black_user}
                alt="icon"
            />
    },
    {
        label: 'Reviewed products', path: '/cabinet/reviewed-products', icon:
            <img
                style={{ width: "20px", height: "20px" }}
                src={black_eye}
                alt="icon"
            />
    },
    {
        label: 'My Order', path: '/cabinet/order', icon: <img
            style={{ width: "20px", height: "20px" }}
            src={black_shopping_cart}
            alt="icon"
        />
    },
    {
        label: 'Selected products', path: '/cabinet/product', icon: <img
            style={{ width: "20px", height: "20px" }}
            src={black_heart}
            alt="icon"
        />
    },
    {
        label: 'My reviews', path: '/cabinet/reviews', icon: <img
            style={{ width: "20px", height: "20px" }}
            src={black_review}
            alt="icon"
        />
    },
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