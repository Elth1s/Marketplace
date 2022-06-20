import Box from '@mui/material/Box';
import Link from '@mui/material/Link';
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemButton from '@mui/material/ListItemButton';
import ListItemIcon from '@mui/material/ListItemIcon';
import ListItemText from '@mui/material/ListItemText';

import NavigateNextIcon from '@mui/icons-material/NavigateNext';

import { FC } from 'react';
import { IDrawer } from './type';

import Logo from '../../../components/Logo';
import DrawerStyle from './styled';

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
                <Logo />
            </Box>
            <List>
                {menuItems.map((item, index) => (
                    <ListItem key={index} component={Link} href={item.path} disablePadding>
                        <ListItemButton>
                            <ListItemIcon>
                                <NavigateNextIcon/>
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