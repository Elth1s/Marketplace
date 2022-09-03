import { Slide } from '@mui/material';
import { TransitionProps } from '@mui/material/transitions';

import { forwardRef } from 'react';
import { AdminSellerDialogStyle } from './styled';

export interface IDialog {
    open: boolean,
    onClose: any,
    dialogContent: any,
};

const Transition = forwardRef(function Transition(
    props: TransitionProps & {
        children: React.ReactElement<any, any>;
    },
    ref: React.Ref<unknown>,
) {
    return <Slide direction="left" ref={ref} {...props} />;
});

const AdminSellerDialog: React.FC<IDialog> = ({ open, onClose, dialogContent }) => {
    return (
        <AdminSellerDialogStyle
            open={open}
            onClose={onClose}
            TransitionComponent={Transition}
        >
            {dialogContent}
        </AdminSellerDialogStyle>
    )
}

export default AdminSellerDialog;