import { keyframes } from "@mui/system";
import { styled } from '@mui/material';
import {
  Drawer,
  ListItemButton
} from '@mui/material';

export const DrawerStyle = styled(Drawer, { shouldForwardProp: (prop) => prop !== 'open' })(
  ({ theme, open }) => ({
    '& .MuiDrawer-paper': {
      background: "transparent",
      position: 'relative',
      whiteSpace: 'nowrap',
      width: 278,
      transition: theme.transitions.create('width', {
        easing: theme.transitions.easing.sharp,
        duration: theme.transitions.duration.enteringScreen,
      }),
      boxSizing: 'border-box',
      ...(!open && {
        borderRight: 'none',
        overflowX: 'hidden',
        transition: theme.transitions.create('width', {
          easing: theme.transitions.easing.sharp,
          duration: theme.transitions.duration.leavingScreen,
        }),
        width: theme.spacing(0),
        [theme.breakpoints.up('sm')]: {
          width: theme.spacing(0),
        },
      }),
    },
  }),
);

export const ListItemButtonStyle = styled(ListItemButton)(({ theme }) => ({
  padding: "14px 0px 14px 24px",
  borderColor: "#E5E5E5",
  borderStyle: "solid",
  borderWidth: "0px 0px 1px 1px"
}));

const rotateRight = keyframes`
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(45deg);
  }
`;
const rotateLeft = keyframes`
  from {
    transform: rotate(45deg);
  }
  to {
    transform: rotate(90deg);
  }
`;


export const RotatedBox = styled("div", {
  shouldForwardProp: (prop) => prop !== "rotete" && prop !== "isRoteted"
})<{ rotete?: boolean; isRoteted?: boolean; }>(({ theme, rotete, isRoteted }) => ({
  backgroundColor: rotete ? theme.palette.primary.main : theme.palette.secondary.main,
  width: 15,
  height: 15,
  marginRight: "10px",
  transform: isRoteted ? "rotate(45deg)" : "",
  ...((rotete != undefined && isRoteted == false) && { animation: rotete ? `${rotateRight} 1s forwards ease` : `${rotateLeft} 1s forwards ease` }),
}));