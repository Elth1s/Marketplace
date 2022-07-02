import {
    Toolbar,
    IconButton,
} from '@mui/material';
import {
    Menu
} from '@mui/icons-material';
import { FC } from 'react';

import { AppBarStyle, LeftBox } from './styled';

import { logo } from '../../../assets/logos';

import LinkRouter from '../../../components/LinkRouter';
import Notification from './Notification'
import Profile from './Profile';

interface IHeader {
    handleDrawerToggle: any,
}

const Header: FC<IHeader> = ({ handleDrawerToggle }) => {
    return (
        <AppBarStyle color="transparent" sx={{ zIndex: 1 }}>
            <Toolbar >
                <LeftBox>
                    <LinkRouter underline="none" color="unset" to="/" >
                        <img
                            style={{ cursor: "pointer", width: "80px" }}
                            src={logo}
                            alt="logo"
                        />
                    </LinkRouter>
                    <IconButton
                        sx={{ borderRadius: '12px' }}
                        onClick={handleDrawerToggle}
                        size="large"
                        aria-label="search"
                        color="inherit"
                    >
                        <Menu />
                    </IconButton>
                </LeftBox>


                {/* <Box sx={{ flexShrink: 0, ml: 0.75 }}>
                    <IconButton size="large" aria-label="search" color="inherit">
                        <Search />
                    </IconButton>
                </Box>
                <Notification />
                <Profile /> */}

            </Toolbar>
        </AppBarStyle>
    )
};

export default Header;