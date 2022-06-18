import Box from '@mui/material/Box';
import Badge from '@mui/material/Badge';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';

import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';

import IconButton from '@mui/material/IconButton';
import NotificationsActiveIcon from '@mui/icons-material/NotificationsActive';

import { useState } from 'react';
import Grid from '@mui/material/Grid';

const count = 10;
const notifications = [
    {
        icon: "icon/store",
        name: "Store Verification Done",
        desc: "Lorem ipsum dolor sit amet, consectetur adipiscing elit."
    },
    {
        icon: "icon/avatar",
        name: "John Doe",
        desc: "Proin sodales justo a tortor viverra, volutpat ultricies nunc egestas."
    },
    {
        icon: "icon/store",
        name: "Check Your Mail.",
        desc: "Donec mattis nisi non magna sollicitudin efficitur."
    },
]

const Notification = () => {
    const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
    const open = Boolean(anchorEl);

    const handleClick = (event: React.MouseEvent<HTMLButtonElement>) => {
        setAnchorEl(event.currentTarget);
    };
    const handleClose = () => {
        setAnchorEl(null);
    };

    return (
        <Box sx={{ flexShrink: 0, ml: 0.75 }}>
            <IconButton
                sx={{ borderRadius: '12px' }}
                onClick={handleClick}
                size="large"
                aria-label="search"
                color="inherit"
            >
                <Badge badgeContent={count} color="error">
                    <NotificationsActiveIcon />
                </Badge>
            </IconButton>

            <Menu
                id="basic-menu"
                anchorEl={anchorEl}
                open={open}
                onClose={handleClose}
                MenuListProps={{
                    'aria-labelledby': 'basic-button',
                }}>
                {notifications.map((notification, index) => (
                    <MenuItem
                        key={index}
                        onClick={handleClose}
                        sx={{
                            display: 'flex',
                            maxWidth: 300,
                            margin: 'auto',
                            p: 2,
                        }}
                    >
                        <Grid container wrap="nowrap" spacing={2}>
                            <Grid item>
                                <Avatar
                                    alt={notification.name}
                                    src={notification.icon}
                                />
                            </Grid>
                            <Grid item xs zeroMinWidth>
                                <Grid item xs>
                                    <Typography variant="h6" gutterBottom component="div" >
                                        {notification.name}
                                    </Typography>
                                </Grid>
                                <Grid item xs zeroMinWidth>
                                    <Typography noWrap>{notification.desc}</Typography>
                                </Grid>
                            </Grid>
                        </Grid>
                    </MenuItem>
                ))}
            </Menu>
        </Box >
    )

}

export default Notification;