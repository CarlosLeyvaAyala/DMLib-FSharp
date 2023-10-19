let asString = printfn "\"%s\""
let asGUID = printfn "// ClientGuid = new Guid(\"%s\")"

[ 1..15 ]
|> List.map (fun _ -> System.Guid.NewGuid().ToString())
|> List.iter asString

// ClientGuid = new Guid("79aa0c2b-3e53-4cc0-9107-cdaeb3326374")
// ClientGuid = new Guid("ce5d35fc-7da5-4131-9261-5543de6c641d")
// ClientGuid = new Guid("8f11b6e4-b95f-4fbe-93af-b5cf3cba2152")
// ClientGuid = new Guid("eb35d5c9-3d41-45fb-b20f-8feac936cae4")
// ClientGuid = new Guid("19885c1e-b0a7-4c69-ae68-357e72d844e9")
// ClientGuid = new Guid("8a7147b2-cd89-4919-b18b-29a8bc3b3dc4")
// ClientGuid = new Guid("3befffdf-f28e-4477-9b87-fa58b2ba9680")
// ClientGuid = new Guid("872ce101-aeae-465f-a767-16cfadd770cc")
// ClientGuid = new Guid("cbc7ddc4-2886-4883-808a-232b7a9973e5")
// ClientGuid = new Guid("d9cbf258-5a9f-4c42-aa86-7bd3fba3b73f")
// ClientGuid = new Guid("9f5f2d4c-ca68-4f81-911d-f1b803c9e16d")
// ClientGuid = new Guid("fcb26f00-3a56-446d-adad-ec2a3e490089")

//"681a6740-4999-4f2b-aeb4-1c3647ae3f8b"
//"f825b022-2456-44ec-b8e4-f054c72f12cc"
//"ced6912d-21d5-4d87-936a-3f6663e2c459"
//"d6d0d30e-cf2f-46e1-b6e3-25135d9af20c"
//"d0f7b0f4-264c-4891-beb1-babf51f6bc1c"
//"f5df895d-58bd-4e3b-99ad-4a6aa59da5ca"
//"e2ce7df7-f45f-4aa9-ac43-402cc994d726"
// "de85cd28-fc37-45d4-bd8c-02555fa8725b"
// "4adc5dc0-e5e9-4437-bfc5-925c5c473065"
// "c8a2ec94-d961-4a57-9613-5a8318239989"
// "41c5935b-eaee-46e3-ac64-cb81b3800784"
// "89fb3b1c-019d-4a0a-9f52-f003e2b33d0e"
// "47d45d4e-aba4-40b7-8e7b-df8384d66da2"
// "2ccb4ae1-aa3a-4f29-9d6c-f94db80622d6"
// "65c96e4b-bdff-4355-b403-71e7c416fed2"
// "f5e411f5-3c8e-41e5-9438-0e7cbb67406a"
// "c99df00a-73e5-42b6-9d92-210ad828621f"
// "6e1551d4-2a26-473d-92c6-497e893f2cfc"
// "8c739697-771c-45d0-b9f9-e26cd6e23596"
// "1fb1e66f-4bc3-46c9-b113-cbbbdbb60b50"
// "8a441a84-7b4a-4aec-bc4f-a5e201c35ad6"
// "4fd4df1e-0610-45b6-9270-2d0a41b4b10a"
