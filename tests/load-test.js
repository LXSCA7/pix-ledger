import http from "k6/http"
import { check, sleep } from 'k6'
import { SharedArray } from 'k6/data'

export const options = {
   stages: [
      { duration: '10s', target: 10 },
      { duration: '30s', target: 50 },
      { duration: '10s', target: 10 },
   ],
   thresholds: { 
      http_req_duration: ["p(95)<500"],
      http_req_failed: ["rate<0.01"]
   },
};

const BASE_URL="http://localhost:5014/api"
 
export function setup() {
   console.log("starting setup! creating accounts");
   const MAX_ACCOUNTS = 20;
   const accounts = []
   for (let i = 0; i < MAX_ACCOUNTS; i++) {
      const createRes = http.post(`${BASE_URL}/account`, JSON.stringify({
         firstName: `User${i}`,
         lastName: "Tester"
      }), { headers: {"Content-Type": "application/json"}});
   
      const accountData = createRes.json();
      const accountId = accountData.id;

      const pixKey = `user${i}@pix.com`;
      http.post(`${BASE_URL}/pix`, JSON.stringify({
         key: pixKey,
         kind: 'email',
         userId: accountId
      }), { headers: { 'Content-Type': 'application/json' } });

      http.post(`${BASE_URL}/transaction/deposit`, JSON.stringify({
         accountId: accountId,
         amount: 1000000
      }), { headers: { 'Content-Type': 'application/json' } });

      accounts.push({ id: accountId, key: pixKey });
   }
   console.log("setup finished");
   return accounts;
}


export default function (accounts) {
   const senderIndex = Math.floor(Math.random() * accounts.length);
   let receiverIndex = Math.floor(Math.random() * accounts.length);

   while (receiverIndex === senderIndex) {
    receiverIndex = Math.floor(Math.random() * accounts.length);
   }

   const sender = accounts[senderIndex];
   const receiver = accounts[receiverIndex];

   const payload = JSON.stringify({
      senderAccountId: sender.id,
      receiverPixKey: receiver.key,
      amount: 1.00
   });

   const params = {
    headers: {
      'Content-Type': 'application/json',
    },
  };

  const res = http.post(`${BASE_URL}/transaction/transfer`, payload, params);

  check(res, {
      'status é 200 (Sucesso)': (r) => r.status === 200,
      'status é 400/500 (Concorrência/Bloqueio)': (r) => r.status !== 200, 
   });1

  sleep(0.1);
}