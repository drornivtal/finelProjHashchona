
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { Avatar } from '@mui/material';





export default function RequestDisplay(props) {
    return (

        <Card sx={{ textAlign: 'right', backgroundColor: '#f2f2f2', borderRadius: 4, maxHeight: '160px',  width: '355px' }}>
            <CardContent >
                <Typography sx={{ fontSize: 15 }}
                    color="#708DD3" >
                    {/* טל ברק - בקשה 1010 */}
                    {props.UserName}  - בקשה {props.ReqId}

                    <span style={{ marginRight: '7%', border: '1px solid #708DD3', borderRadius: 20, backgroundColor: '#708DD3', opacity: 0.8, color: 'black', padding: '1%', paddingLeft: '3%', paddingRight: '3%', fontSize: 14 }}>
                        {/* יום ד' 7/2 | 20:20 */}

                        {props.DueDateHebrewDay}  {props.DueDate} | {props.DueTime}

                    </span>
                </Typography>

                <Typography sx={{ fontSize: 12 }}
                    color="grey" fontWeight={600} gutterBottom>
                    {/* 23/05/24 15:38 */}
                    {props.PostDate} {props.PostTime}
                </Typography>

                <Box sx={{ display: 'flex', flexDirection: 'row', direction: 'rtl', marginTop: '1%' }}>
                    <Avatar sx={{ backgroundColor: '#708DD3' }}>ט</Avatar>
                    <Typography variant="h7" component="h5" sx={{ paddingRight: 1.5, textAlign: 'right' }}>
                        {/* שלום, מחפשת מישהו שיוציא לי את הכלב
                        שלום, מחפשת מישהו שיוציא לי את הכלב שלום, מחפשת מישהו שיוציא */}
                        {props.Description}
                    </Typography>
                </Box>
            </CardContent>
            <CardActions>
                <Button size="small" sx={{ backgroundColor: 'rgb(252, 252, 252)', border: '1px solid black', borderRadius: 20, color: 'black', marginTop: '-9%' }}>הגש סיוע</Button>
            </CardActions>
        </Card>
    );
}





