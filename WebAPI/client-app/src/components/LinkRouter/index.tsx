import { Link } from '@mui/material';
import {
    Link as RouterLink,
} from 'react-router-dom';


const LinkRouter: React.FC<any> = (props) => <Link {...props} component={RouterLink} />;

export default LinkRouter;
