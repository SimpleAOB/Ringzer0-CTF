import itertools
import hashlib

res = itertools.permutations('abcdefghijklmnopqrstuvwxyz0123456789',6)
cnt = 0
tmpdict = {}
for i in res:
   cur = (''.join(i)).encode('utf-8')
   curh = hashlib.sha1(cur).hexdigest()
   if curh[:4] not in tmpdict:
      tmpdict[curh[:4]] = "start;"
   tmpdict[curh[:4]] = tmpdict[curh[:4]] + curh + ":" + cur.decode('utf-8') + ";"
   cnt += 1
   if cnt > 15000000:
      for key, value in tmpdict.items():
         tmpdict[key] = "";
         file = open(key, 'a')
         file.write(value)
         file.close()
      cnt = 0
      print(cur.decode('utf-8'))
