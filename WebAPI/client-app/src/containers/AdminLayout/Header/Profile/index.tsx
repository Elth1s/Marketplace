import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';

import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';

import IconButton from '@mui/material/IconButton';
import ArrowDropDownIcon from '@mui/icons-material/ArrowDropDown';

import { useState } from 'react';

const settings = ['Profile', 'Account', 'Dashboard', 'Logout'];
const name = "Remy Sharp"
const path = "/static/images/avatar/2.jpg"

const Profile = () => {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);

    const handleOpenMenu = (event: React.MouseEvent<HTMLElement>) => {
        setAnchorEl(event.currentTarget);
    };

    const handleCloseMenu = () => {
        setAnchorEl(null);
    };

    return (
        <Box sx={{ flexShrink: 0, ml: 0.75 }}>
            <IconButton
                sx={{ borderRadius: '12px' }}
                onClick={handleOpenMenu}

            >
                <Avatar
                    alt={name}
                    src={path}
                />
                <Typography sx={{ mx: 1 }}>
                    {name}
                </Typography>
                <ArrowDropDownIcon />
            </IconButton>
            <Menu
                sx={{ mt: '45px' }}
                id="menu-appbar"
                anchorEl={anchorEl}
                anchorOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                keepMounted
                transformOrigin={{
                    vertical: 'top',
                    horizontal: 'right',
                }}
                open={Boolean(anchorEl)}
                onClose={handleCloseMenu}
            >
                {settings.map((setting) => (
                    <MenuItem key={setting} onClick={handleCloseMenu}>
                        <Typography textAlign="center">{setting}</Typography>
                    </MenuItem>
                ))}
            </Menu>
        </Box>
    );
}

export default Profile;