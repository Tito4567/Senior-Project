﻿namespace LacesViewModel.Request
{
    public class UpdatePasswordRequest : LacesRequest
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
