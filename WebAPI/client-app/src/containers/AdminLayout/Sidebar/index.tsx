import Box from '@mui/material/Box';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';

import NavigateNextIcon from '@mui/icons-material/NavigateNext';

import { FC } from 'react';
import { Link } from "react-router-dom";

import Logo from '../../../components/Logo';
import DrawerStyle from './styled';

import { IDrawer } from './type';
import { logo } from '../../../assets/logos';

const menuItems = [
    { lable: 'Category', path: '/admin/category', },
    { lable: 'Characteristic Group', path: '/admin/characteristicGroup', },
    { lable: 'Characteristic', path: '/admin/characteristic', },
    { lable: 'Country', path: '/admin/country', },
    { lable: 'City', path: '/admin/city', },
];

const Sitebar: FC<IDrawer> = ({ open }) => {
    return (
        <DrawerStyle variant="permanent" open={open} >
            <Box
                sx={{
                    display: 'inline-flex',
                    alignItems: 'center',
                    justifyContent: 'center',
                    p: 1,
                }}
            >
                <Link to="/" style={{ textDecoration: 'none', color: 'unset' }}>
                    <img

                        style={{ cursor: "pointer", width: "196px" }}
                        src={logo}
                        alt="logo"
                    />
                </Link>
            </Box>
            <List>
                {menuItems.map((item, index) => (
                    <ListItem key={index} component={Link} to={item.path} disablePadding style={{ textDecoration: 'none', color: 'unset' }} >
                        <ListItemButton>
                            <ListItemIcon>
                                <NavigateNextIcon />
                            </ListItemIcon>
                            <ListItemText primary={item.lable} />
                        </ListItemButton>
                    </ListItem>
                ))}
            </List>
        </DrawerStyle>
    );
};

export default Sitebar;